using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F07 RID: 3847
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoBindingsException : LocalizedException
	{
		// Token: 0x0600AA0C RID: 43532 RVA: 0x0028CB62 File Offset: 0x0028AD62
		public NoBindingsException(string fqdn) : base(Strings.NoBindings(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600AA0D RID: 43533 RVA: 0x0028CB77 File Offset: 0x0028AD77
		public NoBindingsException(string fqdn, Exception innerException) : base(Strings.NoBindings(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600AA0E RID: 43534 RVA: 0x0028CB8D File Offset: 0x0028AD8D
		protected NoBindingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x0600AA0F RID: 43535 RVA: 0x0028CBB7 File Offset: 0x0028ADB7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x1700370D RID: 14093
		// (get) Token: 0x0600AA10 RID: 43536 RVA: 0x0028CBD2 File Offset: 0x0028ADD2
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x04006073 RID: 24691
		private readonly string fqdn;
	}
}
