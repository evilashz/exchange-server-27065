using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000739 RID: 1849
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		// Token: 0x06005202 RID: 20994 RVA: 0x0011F29B File Offset: 0x0011D49B
		public SoapFault()
		{
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0011F2A3 File Offset: 0x0011D4A3
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultActor = faultActor;
			this.detail = serverFault;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0011F2C8 File Offset: 0x0011D4C8
		internal SoapFault(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				object value = enumerator.Value;
				if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
				{
					int num = ((string)value).IndexOf(':');
					if (num > -1)
					{
						this.faultCode = ((string)value).Substring(num + 1);
					}
					else
					{
						this.faultCode = (string)value;
					}
				}
				else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultString = (string)value;
				}
				else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultActor = (string)value;
				}
				else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
				{
					this.detail = value;
				}
			}
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0011F3A8 File Offset: 0x0011D5A8
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", "SOAP-ENV:" + this.faultCode);
			info.AddValue("faultstring", this.faultString);
			if (this.faultActor != null)
			{
				info.AddValue("faultactor", this.faultActor);
			}
			info.AddValue("detail", this.detail, typeof(object));
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x0011F415 File Offset: 0x0011D615
		// (set) Token: 0x06005207 RID: 20999 RVA: 0x0011F41D File Offset: 0x0011D61D
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x0011F426 File Offset: 0x0011D626
		// (set) Token: 0x06005209 RID: 21001 RVA: 0x0011F42E File Offset: 0x0011D62E
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x0011F437 File Offset: 0x0011D637
		// (set) Token: 0x0600520B RID: 21003 RVA: 0x0011F43F File Offset: 0x0011D63F
		public string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600520C RID: 21004 RVA: 0x0011F448 File Offset: 0x0011D648
		// (set) Token: 0x0600520D RID: 21005 RVA: 0x0011F450 File Offset: 0x0011D650
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		// Token: 0x04002421 RID: 9249
		private string faultCode;

		// Token: 0x04002422 RID: 9250
		private string faultString;

		// Token: 0x04002423 RID: 9251
		private string faultActor;

		// Token: 0x04002424 RID: 9252
		[SoapField(Embedded = true)]
		private object detail;
	}
}
