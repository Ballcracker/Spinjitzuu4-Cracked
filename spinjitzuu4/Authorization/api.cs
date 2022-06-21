using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace spinjitzuu4.Authorization
{
	// Token: 0x0200002E RID: 46
	public class api
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000180BC File Offset: 0x000162BC
		public api(string name, string ownerid, string secret, string version)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
			{
				api.error("Application not setup correctly. Please watch video link found in Program.cs");
				Environment.Exit(0);
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00018154 File Offset: 0x00016354
		public void init()
		{
			this.enckey = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init"));
			nameValueCollection["ver"] = encryption.encrypt(this.version, this.secret, text);
			nameValueCollection["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName);
			nameValueCollection["enckey"] = encryption.encrypt(this.enckey, this.secret, text);
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			if (text2 == "KeyAuth_Invalid")
			{
				api.error("Application not found");
				Environment.Exit(0);
			}
			text2 = encryption.decrypt(text2, this.secret, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_app_data(response_structure.appinfo);
				this.sessionid = response_structure.sessionid;
				this.initzalized = true;
				return;
			}
			if (response_structure.message == "invalidver")
			{
				this.app_data.downloadLink = response_structure.download;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000182E0 File Offset: 0x000164E0
		public void register(string username, string pass, string key)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("register"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00018458 File Offset: 0x00016658
		public void login(string username, string pass)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("login"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000185B8 File Offset: 0x000167B8
		public void web_login()
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			HttpListener httpListener = new HttpListener();
			string text = "handshake";
			text = "http://localhost:1337/" + text + "/";
			httpListener.Prefixes.Add(text);
			httpListener.Start();
			HttpListenerContext context = httpListener.GetContext();
			HttpListenerRequest request = context.Request;
			HttpListenerResponse httpListenerResponse = context.Response;
			httpListenerResponse.AddHeader("Access-Control-Allow-Methods", "GET, POST");
			httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
			httpListenerResponse.AddHeader("Via", "hugzho's big brain");
			httpListenerResponse.AddHeader("Location", "your kernel ;)");
			httpListenerResponse.AddHeader("Retry-After", "never lmao");
			httpListenerResponse.Headers.Add("Server", "\r\n\r\n");
			httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
			httpListener.UnsafeConnectionNtlmAuthentication = true;
			httpListener.IgnoreWriteExceptions = true;
			string text2 = request.RawUrl.Replace("/handshake?user=", "").Replace("&token=", " ");
			string value2 = text2.Split(Array.Empty<char>())[0];
			string value3 = text2.Split(new char[]
			{
				' '
			})[1];
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = "login";
			nameValueCollection["username"] = value2;
			nameValueCollection["token"] = value3;
			nameValueCollection["hwid"] = value;
			nameValueCollection["sessionid"] = this.sessionid;
			nameValueCollection["name"] = this.name;
			nameValueCollection["ownerid"] = this.ownerid;
			string json = api.req_unenc(nameValueCollection);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(json);
			this.load_response_struct(response_structure);
			bool flag = true;
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
				httpListenerResponse.StatusCode = 420;
				httpListenerResponse.StatusDescription = "SHEESH";
			}
			else
			{
				Console.WriteLine(response_structure.message);
				httpListenerResponse.StatusCode = 200;
				httpListenerResponse.StatusDescription = response_structure.message;
				flag = false;
			}
			byte[] bytes = Encoding.UTF8.GetBytes("Whats up?");
			httpListenerResponse.ContentLength64 = (long)bytes.Length;
			httpListenerResponse.OutputStream.Write(bytes, 0, bytes.Length);
			Thread.Sleep(1250);
			httpListener.Stop();
			if (!flag)
			{
				Environment.Exit(0);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00018848 File Offset: 0x00016A48
		public void button(string button)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			HttpListener httpListener = new HttpListener();
			string uriPrefix = "http://localhost:1337/" + button + "/";
			httpListener.Prefixes.Add(uriPrefix);
			httpListener.Start();
			HttpListenerContext context = httpListener.GetContext();
			HttpListenerRequest request = context.Request;
			HttpListenerResponse httpListenerResponse = context.Response;
			httpListenerResponse.AddHeader("Access-Control-Allow-Methods", "GET, POST");
			httpListenerResponse.AddHeader("Access-Control-Allow-Origin", "*");
			httpListenerResponse.AddHeader("Via", "hugzho's big brain");
			httpListenerResponse.AddHeader("Location", "your kernel ;)");
			httpListenerResponse.AddHeader("Retry-After", "never lmao");
			httpListenerResponse.Headers.Add("Server", "\r\n\r\n");
			httpListenerResponse.StatusCode = 420;
			httpListenerResponse.StatusDescription = "SHEESH";
			httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
			httpListener.UnsafeConnectionNtlmAuthentication = true;
			httpListener.IgnoreWriteExceptions = true;
			httpListener.Stop();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00018950 File Offset: 0x00016B50
		public void upgrade(string username, string key)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("upgrade"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			response_structure.success = false;
			this.load_response_struct(response_structure);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00018A88 File Offset: 0x00016C88
		public void license(string key)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00018BD0 File Offset: 0x00016DD0
		public void check()
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("check"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public void setvar(string var, string data)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("setvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["data"] = encryption.encrypt(data, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data2 = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data2);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public string getvar(string var)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("getvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
			return null;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00018EFC File Offset: 0x000170FC
		public void ban()
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("ban"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00018FEC File Offset: 0x000171EC
		public string var(string varid)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.message;
			}
			return null;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00019114 File Offset: 0x00017314
		public List<api.msg> chatget(string channelname)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatget"));
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.messages;
			}
			return null;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0001922C File Offset: 0x0001742C
		public bool chatsend(string msg, string channelname)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatsend"));
			nameValueCollection["message"] = encryption.encrypt(msg, this.enckey, text);
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00019358 File Offset: 0x00017558
		public bool checkblack()
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("checkblacklist"));
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0001947C File Offset: 0x0001767C
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
				return null;
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("webhook"));
			nameValueCollection["webid"] = encryption.encrypt(webid, this.enckey, text);
			nameValueCollection["params"] = encryption.encrypt(param, this.enckey, text);
			nameValueCollection["body"] = encryption.encrypt(body, this.enckey, text);
			nameValueCollection["conttype"] = encryption.encrypt(conttype, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
			return null;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000195E4 File Offset: 0x000177E4
		public byte[] download(string fileid)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first. File is empty since no request could be made.");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return encryption.str_to_byte_arr(response_structure.contents);
			}
			return null;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00019704 File Offset: 0x00017904
		public void log(string message)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("log"));
			nameValueCollection["pcuser"] = encryption.encrypt(Environment.UserName, this.enckey, text);
			nameValueCollection["message"] = encryption.encrypt(message, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			api.req(nameValueCollection);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00019808 File Offset: 0x00017A08
		public static string checksum(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					result = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00019878 File Offset: 0x00017A78
		public static void error(string message)
		{
			Process.Start(new ProcessStartInfo("cmd.exe", "/c start cmd /C \"color b && title Error && echo " + message + " && timeout /t 5\"")
			{
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false
			});
			Environment.Exit(0);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000198CC File Offset: 0x00017ACC
		private static string req(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.0/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
				{
					api.error("You're connecting too fast to loader, slow down.");
					Environment.Exit(0);
					result = "";
				}
				else
				{
					api.error("Connection failure. Please try again, or contact us for help.");
					Environment.Exit(0);
					result = "";
				}
			}
			return result;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0001996C File Offset: 0x00017B6C
		private static string req_unenc(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.1/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
				{
					Thread.Sleep(1000);
					result = api.req(post_data);
				}
				else
				{
					api.error("Connection failure. Please try again, or contact us for help.");
					Environment.Exit(0);
					result = "";
				}
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00019A04 File Offset: 0x00017C04
		private void load_app_data(api.app_data_structure data)
		{
			this.app_data.numUsers = data.numUsers;
			this.app_data.numOnlineUsers = data.numOnlineUsers;
			this.app_data.numKeys = data.numKeys;
			this.app_data.version = data.version;
			this.app_data.customerPanelLink = data.customerPanelLink;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00019A68 File Offset: 0x00017C68
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = data.username;
			this.user_data.ip = data.ip;
			this.user_data.hwid = data.hwid;
			this.user_data.createdate = data.createdate;
			this.user_data.lastlogin = data.lastlogin;
			this.user_data.subscriptions = data.subscriptions;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002FD8 File Offset: 0x000011D8
		private void load_response_struct(api.response_structure data)
		{
			this.response.success = data.success;
			this.response.message = data.message;
		}

		// Token: 0x040003DA RID: 986
		public string name;

		// Token: 0x040003DB RID: 987
		public string ownerid;

		// Token: 0x040003DC RID: 988
		public string secret;

		// Token: 0x040003DD RID: 989
		public string version;

		// Token: 0x040003DE RID: 990
		private string sessionid;

		// Token: 0x040003DF RID: 991
		private string enckey;

		// Token: 0x040003E0 RID: 992
		private bool initzalized;

		// Token: 0x040003E1 RID: 993
		public api.app_data_class app_data = new api.app_data_class();

		// Token: 0x040003E2 RID: 994
		public api.user_data_class user_data = new api.user_data_class();

		// Token: 0x040003E3 RID: 995
		public api.response_class response = new api.response_class();

		// Token: 0x040003E4 RID: 996
		private json_wrapper response_decoder = new json_wrapper(new api.response_structure());

		// Token: 0x0200002F RID: 47
		[DataContract]
		private class response_structure
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000157 RID: 343 RVA: 0x00002FFC File Offset: 0x000011FC
			// (set) Token: 0x06000158 RID: 344 RVA: 0x00003004 File Offset: 0x00001204
			[DataMember]
			public bool success { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000159 RID: 345 RVA: 0x0000300D File Offset: 0x0000120D
			// (set) Token: 0x0600015A RID: 346 RVA: 0x00003015 File Offset: 0x00001215
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600015B RID: 347 RVA: 0x0000301E File Offset: 0x0000121E
			// (set) Token: 0x0600015C RID: 348 RVA: 0x00003026 File Offset: 0x00001226
			[DataMember]
			public string contents { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600015D RID: 349 RVA: 0x0000302F File Offset: 0x0000122F
			// (set) Token: 0x0600015E RID: 350 RVA: 0x00003037 File Offset: 0x00001237
			[DataMember]
			public string response { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600015F RID: 351 RVA: 0x00003040 File Offset: 0x00001240
			// (set) Token: 0x06000160 RID: 352 RVA: 0x00003048 File Offset: 0x00001248
			[DataMember]
			public string message { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000161 RID: 353 RVA: 0x00003051 File Offset: 0x00001251
			// (set) Token: 0x06000162 RID: 354 RVA: 0x00003059 File Offset: 0x00001259
			[DataMember]
			public string download { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000163 RID: 355 RVA: 0x00003062 File Offset: 0x00001262
			// (set) Token: 0x06000164 RID: 356 RVA: 0x0000306A File Offset: 0x0000126A
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00003073 File Offset: 0x00001273
			// (set) Token: 0x06000166 RID: 358 RVA: 0x0000307B File Offset: 0x0000127B
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000167 RID: 359 RVA: 0x00003084 File Offset: 0x00001284
			// (set) Token: 0x06000168 RID: 360 RVA: 0x0000308C File Offset: 0x0000128C
			[DataMember]
			public List<api.msg> messages { get; set; }
		}

		// Token: 0x02000030 RID: 48
		public class msg
		{
			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600016B RID: 363 RVA: 0x00003095 File Offset: 0x00001295
			// (set) Token: 0x0600016C RID: 364 RVA: 0x0000309D File Offset: 0x0000129D
			public string message { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600016D RID: 365 RVA: 0x000030A6 File Offset: 0x000012A6
			// (set) Token: 0x0600016E RID: 366 RVA: 0x000030AE File Offset: 0x000012AE
			public string author { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600016F RID: 367 RVA: 0x000030B7 File Offset: 0x000012B7
			// (set) Token: 0x06000170 RID: 368 RVA: 0x000030BF File Offset: 0x000012BF
			public string timestamp { get; set; }
		}

		// Token: 0x02000031 RID: 49
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000173 RID: 371 RVA: 0x000030C8 File Offset: 0x000012C8
			// (set) Token: 0x06000174 RID: 372 RVA: 0x000030D0 File Offset: 0x000012D0
			[DataMember]
			public string username { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000175 RID: 373 RVA: 0x000030D9 File Offset: 0x000012D9
			// (set) Token: 0x06000176 RID: 374 RVA: 0x000030E1 File Offset: 0x000012E1
			[DataMember]
			public string ip { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000177 RID: 375 RVA: 0x000030EA File Offset: 0x000012EA
			// (set) Token: 0x06000178 RID: 376 RVA: 0x000030F2 File Offset: 0x000012F2
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000179 RID: 377 RVA: 0x000030FB File Offset: 0x000012FB
			// (set) Token: 0x0600017A RID: 378 RVA: 0x00003103 File Offset: 0x00001303
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600017B RID: 379 RVA: 0x0000310C File Offset: 0x0000130C
			// (set) Token: 0x0600017C RID: 380 RVA: 0x00003114 File Offset: 0x00001314
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600017D RID: 381 RVA: 0x0000311D File Offset: 0x0000131D
			// (set) Token: 0x0600017E RID: 382 RVA: 0x00003125 File Offset: 0x00001325
			[DataMember]
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000032 RID: 50
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000181 RID: 385 RVA: 0x0000312E File Offset: 0x0000132E
			// (set) Token: 0x06000182 RID: 386 RVA: 0x00003136 File Offset: 0x00001336
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000183 RID: 387 RVA: 0x0000313F File Offset: 0x0000133F
			// (set) Token: 0x06000184 RID: 388 RVA: 0x00003147 File Offset: 0x00001347
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000185 RID: 389 RVA: 0x00003150 File Offset: 0x00001350
			// (set) Token: 0x06000186 RID: 390 RVA: 0x00003158 File Offset: 0x00001358
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000187 RID: 391 RVA: 0x00003161 File Offset: 0x00001361
			// (set) Token: 0x06000188 RID: 392 RVA: 0x00003169 File Offset: 0x00001369
			[DataMember]
			public string version { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x06000189 RID: 393 RVA: 0x00003172 File Offset: 0x00001372
			// (set) Token: 0x0600018A RID: 394 RVA: 0x0000317A File Offset: 0x0000137A
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600018B RID: 395 RVA: 0x00003183 File Offset: 0x00001383
			// (set) Token: 0x0600018C RID: 396 RVA: 0x0000318B File Offset: 0x0000138B
			[DataMember]
			public string downloadLink { get; set; }
		}

		// Token: 0x02000033 RID: 51
		public class app_data_class
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600018F RID: 399 RVA: 0x00003194 File Offset: 0x00001394
			// (set) Token: 0x06000190 RID: 400 RVA: 0x0000319C File Offset: 0x0000139C
			public string numUsers { get; set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000191 RID: 401 RVA: 0x000031A5 File Offset: 0x000013A5
			// (set) Token: 0x06000192 RID: 402 RVA: 0x000031AD File Offset: 0x000013AD
			public string numOnlineUsers { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000193 RID: 403 RVA: 0x000031B6 File Offset: 0x000013B6
			// (set) Token: 0x06000194 RID: 404 RVA: 0x000031BE File Offset: 0x000013BE
			public string numKeys { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000195 RID: 405 RVA: 0x000031C7 File Offset: 0x000013C7
			// (set) Token: 0x06000196 RID: 406 RVA: 0x000031CF File Offset: 0x000013CF
			public string version { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000197 RID: 407 RVA: 0x000031D8 File Offset: 0x000013D8
			// (set) Token: 0x06000198 RID: 408 RVA: 0x000031E0 File Offset: 0x000013E0
			public string customerPanelLink { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000199 RID: 409 RVA: 0x000031E9 File Offset: 0x000013E9
			// (set) Token: 0x0600019A RID: 410 RVA: 0x000031F1 File Offset: 0x000013F1
			public string downloadLink { get; set; }
		}

		// Token: 0x02000034 RID: 52
		public class user_data_class
		{
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600019D RID: 413 RVA: 0x000031FA File Offset: 0x000013FA
			// (set) Token: 0x0600019E RID: 414 RVA: 0x00003202 File Offset: 0x00001402
			public string username { get; set; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600019F RID: 415 RVA: 0x0000320B File Offset: 0x0000140B
			// (set) Token: 0x060001A0 RID: 416 RVA: 0x00003213 File Offset: 0x00001413
			public string ip { get; set; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000321C File Offset: 0x0000141C
			// (set) Token: 0x060001A2 RID: 418 RVA: 0x00003224 File Offset: 0x00001424
			public string hwid { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000322D File Offset: 0x0000142D
			// (set) Token: 0x060001A4 RID: 420 RVA: 0x00003235 File Offset: 0x00001435
			public string createdate { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000323E File Offset: 0x0000143E
			// (set) Token: 0x060001A6 RID: 422 RVA: 0x00003246 File Offset: 0x00001446
			public string lastlogin { get; set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000324F File Offset: 0x0000144F
			// (set) Token: 0x060001A8 RID: 424 RVA: 0x00003257 File Offset: 0x00001457
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000035 RID: 53
		public class Data
		{
			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060001AB RID: 427 RVA: 0x00003260 File Offset: 0x00001460
			// (set) Token: 0x060001AC RID: 428 RVA: 0x00003268 File Offset: 0x00001468
			public string subscription { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060001AD RID: 429 RVA: 0x00003271 File Offset: 0x00001471
			// (set) Token: 0x060001AE RID: 430 RVA: 0x00003279 File Offset: 0x00001479
			public string expiry { get; set; }

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060001AF RID: 431 RVA: 0x00003282 File Offset: 0x00001482
			// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000328A File Offset: 0x0000148A
			public string timeleft { get; set; }
		}

		// Token: 0x02000036 RID: 54
		public class response_class
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003293 File Offset: 0x00001493
			// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000329B File Offset: 0x0000149B
			public bool success { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x000032A4 File Offset: 0x000014A4
			// (set) Token: 0x060001B6 RID: 438 RVA: 0x000032AC File Offset: 0x000014AC
			public string message { get; set; }
		}
	}
}
