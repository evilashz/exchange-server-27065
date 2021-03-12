using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F06 RID: 3846
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoReceiveConnectorsException : LocalizedException
	{
		// Token: 0x0600AA07 RID: 43527 RVA: 0x0028CAEA File Offset: 0x0028ACEA
		public NoReceiveConnectorsException(string fqdn) : base(Strings.NoReceiveConnectors(fqdn))
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600AA08 RID: 43528 RVA: 0x0028CAFF File Offset: 0x0028ACFF
		public NoReceiveConnectorsException(string fqdn, Exception innerException) : base(Strings.NoReceiveConnectors(fqdn), innerException)
		{
			this.fqdn = fqdn;
		}

		// Token: 0x0600AA09 RID: 43529 RVA: 0x0028CB15 File Offset: 0x0028AD15
		protected NoReceiveConnectorsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fqdn = (string)info.GetValue("fqdn", typeof(string));
		}

		// Token: 0x0600AA0A RID: 43530 RVA: 0x0028CB3F File Offset: 0x0028AD3F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fqdn", this.fqdn);
		}

		// Token: 0x1700370C RID: 14092
		// (get) Token: 0x0600AA0B RID: 43531 RVA: 0x0028CB5A File Offset: 0x0028AD5A
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x04006072 RID: 24690
		private readonly string fqdn;
	}
}
