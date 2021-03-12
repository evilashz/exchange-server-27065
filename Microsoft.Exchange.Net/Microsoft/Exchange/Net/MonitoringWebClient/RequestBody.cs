using System;
using System.IO;
using System.Security;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007E5 RID: 2021
	internal class RequestBody
	{
		// Token: 0x06002A47 RID: 10823 RVA: 0x0005BAF0 File Offset: 0x00059CF0
		private RequestBody(string bodyFormat, RequestBody.RequestBodyItemWrapper[] parameters)
		{
			this.bodyFormat = bodyFormat;
			this.parameters = parameters;
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0005BB08 File Offset: 0x00059D08
		public static RequestBody Format(string bodyFormat, params object[] parameters)
		{
			RequestBody.RequestBodyItemWrapper[] array = null;
			if (parameters != null && parameters.Length > 0)
			{
				array = new RequestBody.RequestBodyItemWrapper[parameters.Length];
				int num = 0;
				foreach (object wrappedObject in parameters)
				{
					array[num] = RequestBody.RequestBodyItemWrapper.Create(wrappedObject);
					num++;
				}
			}
			return new RequestBody(bodyFormat, array);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0005BB58 File Offset: 0x00059D58
		public override string ToString()
		{
			if (this.parameters != null)
			{
				return string.Format(this.bodyFormat, this.parameters);
			}
			return this.bodyFormat;
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x0005BB7C File Offset: 0x00059D7C
		public void Write(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				string text = this.ToString();
				string[] array = text.Split(new string[]
				{
					"<...>"
				}, StringSplitOptions.None);
				int i = 0;
				foreach (string value in array)
				{
					streamWriter.Write(value);
					if (this.parameters != null)
					{
						while (i < this.parameters.Length)
						{
							if (this.parameters[i].Value is SecureString)
							{
								this.parameters[i].Write(streamWriter);
								i++;
								break;
							}
							i++;
						}
					}
				}
			}
		}

		// Token: 0x040024F9 RID: 9465
		private readonly string bodyFormat;

		// Token: 0x040024FA RID: 9466
		private RequestBody.RequestBodyItemWrapper[] parameters;

		// Token: 0x020007E6 RID: 2022
		internal class RequestBodyItemWrapper
		{
			// Token: 0x06002A4B RID: 10827 RVA: 0x0005BC38 File Offset: 0x00059E38
			private RequestBodyItemWrapper()
			{
			}

			// Token: 0x17000B30 RID: 2864
			// (get) Token: 0x06002A4C RID: 10828 RVA: 0x0005BC40 File Offset: 0x00059E40
			// (set) Token: 0x06002A4D RID: 10829 RVA: 0x0005BC48 File Offset: 0x00059E48
			public object Value { get; private set; }

			// Token: 0x17000B31 RID: 2865
			// (get) Token: 0x06002A4E RID: 10830 RVA: 0x0005BC51 File Offset: 0x00059E51
			// (set) Token: 0x06002A4F RID: 10831 RVA: 0x0005BC59 File Offset: 0x00059E59
			public bool UrlEncode { get; private set; }

			// Token: 0x06002A50 RID: 10832 RVA: 0x0005BC62 File Offset: 0x00059E62
			public static RequestBody.RequestBodyItemWrapper Create(object wrappedObject)
			{
				if (wrappedObject is RequestBody.RequestBodyItemWrapper)
				{
					return wrappedObject as RequestBody.RequestBodyItemWrapper;
				}
				if (wrappedObject is SecureString)
				{
					return RequestBody.RequestBodyItemWrapper.Create(wrappedObject, true);
				}
				return RequestBody.RequestBodyItemWrapper.Create(wrappedObject, false);
			}

			// Token: 0x06002A51 RID: 10833 RVA: 0x0005BC8C File Offset: 0x00059E8C
			public static RequestBody.RequestBodyItemWrapper Create(object wrappedObject, bool urlEncode)
			{
				if (wrappedObject is RequestBody.RequestBodyItemWrapper)
				{
					throw new ArgumentException("wrappedObject is already of type RequestBodyItemWrapper");
				}
				return new RequestBody.RequestBodyItemWrapper
				{
					Value = wrappedObject,
					UrlEncode = urlEncode
				};
			}

			// Token: 0x06002A52 RID: 10834 RVA: 0x0005BCC4 File Offset: 0x00059EC4
			public override string ToString()
			{
				if (this.Value is SecureString)
				{
					return "<...>";
				}
				if (this.Value == null)
				{
					return "<null>";
				}
				if (this.UrlEncode)
				{
					return HttpUtility.UrlEncode(this.Value.ToString());
				}
				return this.Value.ToString();
			}

			// Token: 0x06002A53 RID: 10835 RVA: 0x0005BD18 File Offset: 0x00059F18
			internal void Write(StreamWriter writer)
			{
				if (this.Value is SecureString)
				{
					byte[] array = null;
					char[] array2 = null;
					try
					{
						array = (this.Value as SecureString).ConvertToByteArray();
						if (this.UrlEncode)
						{
							char[] chars = Encoding.Unicode.GetChars(array);
							byte[] bytes = Encoding.UTF8.GetBytes(chars);
							Array.Clear(array, 0, array.Length);
							Array.Clear(chars, 0, chars.Length);
							array = bytes;
							byte[] array3 = HttpUtility.UrlEncodeToBytes(array);
							Array.Clear(array, 0, array.Length);
							array = array3;
						}
						array2 = Encoding.UTF8.GetChars(array);
						writer.Write(array2);
						return;
					}
					finally
					{
						if (array != null)
						{
							Array.Clear(array, 0, array.Length);
						}
						if (array2 != null)
						{
							Array.Clear(array2, 0, array2.Length);
						}
					}
				}
				if (this.Value != null)
				{
					if (this.UrlEncode)
					{
						writer.Write(HttpUtility.UrlEncode(this.Value.ToString()));
						return;
					}
					writer.Write(this.Value);
				}
			}

			// Token: 0x040024FB RID: 9467
			public const string SecureStringReplacement = "<...>";
		}
	}
}
