using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace spinjitzuu4.Authorization
{
	// Token: 0x02000038 RID: 56
	public class json_wrapper
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000032D1 File Offset: 0x000014D1
		public static bool is_serializable(Type to_check)
		{
			return to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00019DE8 File Offset: 0x00017FE8
		public json_wrapper(object obj_to_work_with)
		{
			this.current_object = obj_to_work_with;
			Type type = this.current_object.GetType();
			this.serializer = new DataContractJsonSerializer(type);
			if (!json_wrapper.is_serializable(type))
			{
				throw new Exception(string.Format("the object {0} isn't a serializable", this.current_object));
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00019E3C File Offset: 0x0001803C
		public object string_to_object(string json)
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(json)))
			{
				result = this.serializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000032EF File Offset: 0x000014EF
		public T string_to_generic<T>(string json)
		{
			return (T)((object)this.string_to_object(json));
		}

		// Token: 0x0400040E RID: 1038
		private DataContractJsonSerializer serializer;

		// Token: 0x0400040F RID: 1039
		private object current_object;
	}
}
