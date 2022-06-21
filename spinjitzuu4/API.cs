using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;

namespace spinjitzuu4
{
	// Token: 0x02000006 RID: 6
	internal class API
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00003568 File Offset: 0x00001768
		public static string Scrape()
		{
			string result;
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
				string address = Uri.EscapeUriString("https://127.0.0.1:2999/liveclientdata/allgamedata");
				string text = "";
				using (WebClient webClient = new WebClient())
				{
					text = webClient.DownloadString(address);
				}
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003600 File Offset: 0x00001800
		public static string ScrapeActivePlayer()
		{
			string result;
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
				string address = Uri.EscapeUriString("https://127.0.0.1:2999/liveclientdata/activeplayer");
				string text = "";
				using (WebClient webClient = new WebClient())
				{
					text = webClient.DownloadString(address);
				}
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B9 File Offset: 0x000004B9
		public static string readAbilityLevel(char spell)
		{
			return API.getBetween(API.ScrapeActivePlayer(), spell.ToString() + ": {                \"abilityLevel\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022EE File Offset: 0x000004EE
		public static string readAbilityHaste()
		{
			return API.getBetween(API.ScrapeActivePlayer(), "abilityHaste\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002317 File Offset: 0x00000517
		public static string readActiveCurrentHealth()
		{
			return API.getBetween(API.ScrapeActivePlayer(), "currentHealth\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002340 File Offset: 0x00000540
		public static string readActiveMaxHealth()
		{
			return API.getBetween(API.ScrapeActivePlayer(), "maxHealth\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002369 File Offset: 0x00000569
		public static string readAttackSpeed()
		{
			return API.getBetween(API.Scrape(), "attackSpeed\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002392 File Offset: 0x00000592
		public static string readAttackRange()
		{
			return API.getBetween(API.Scrape(), "attackRange\": ", ".0,");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023AA File Offset: 0x000005AA
		public static string readGameTime()
		{
			return API.getBetween(API.Scrape(), "gameTime\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D3 File Offset: 0x000005D3
		public static string readChampionName()
		{
			return API.getBetween(API.Scrape(), "game_character_displayname_", "\"");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023EB File Offset: 0x000005EB
		public static string readCurrentHealth()
		{
			return API.getBetween(API.Scrape(), "currentHealth\": ", ",").Replace(".", ",");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002414 File Offset: 0x00000614
		public static string readCurrentMana()
		{
			return API.getBetween(API.Scrape(), "resourceValue\": ", ",").Replace(".", ",");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000243D File Offset: 0x0000063D
		public static float GetResourceMax()
		{
			return API.GetActivePlayerData()["championStats"]["resourceMax"].ToObject<float>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000245F File Offset: 0x0000065F
		public static float GetResourceValue()
		{
			return API.GetActivePlayerData()["championStats"]["resourceValue"].ToObject<float>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003698 File Offset: 0x00001898
		public static string getBetween(string strSource, string strStart, string strEnd)
		{
			if (strSource.Contains(strStart) && strSource.Contains(strEnd))
			{
				int num = strSource.IndexOf(strStart, 0) + strStart.Length;
				int num2 = strSource.IndexOf(strEnd, num);
				return strSource.Substring(num, num2 - num);
			}
			return "";
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000036E0 File Offset: 0x000018E0
		public static JObject GetActivePlayerData()
		{
			if (API.IsLiveGameRunning())
			{
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)((HttpWebRequest)WebRequest.Create("https://127.0.0.1:2999/liveclientdata/activeplayer")).GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							try
							{
								return JObject.Parse(streamReader.ReadToEnd());
							}
							catch (Exception)
							{
								throw new Exception("PlayerDataParseFailedException");
							}
						}
					}
				}
			}
			throw new Exception("PlayerDataParseFailedException");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000037A8 File Offset: 0x000019A8
		public static bool IsLiveGameRunning()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://127.0.0.1:2999/liveclientdata/allgamedata");
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
			httpWebRequest.Method = "GET";
			bool result = false;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					if (httpWebResponse.StatusCode == HttpStatusCode.OK)
					{
						result = true;
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}
	}
}
