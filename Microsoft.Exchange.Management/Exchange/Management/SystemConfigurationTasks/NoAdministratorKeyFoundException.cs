using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B9 RID: 4281
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoAdministratorKeyFoundException : FederationException
	{
		// Token: 0x0600B28A RID: 45706 RVA: 0x00299D1B File Offset: 0x00297F1B
		public NoAdministratorKeyFoundException(string trustName) : base(Strings.ErrorNoAdministratorKeyFound(trustName))
		{
			this.trustName = trustName;
		}

		// Token: 0x0600B28B RID: 45707 RVA: 0x00299D30 File Offset: 0x00297F30
		public NoAdministratorKeyFoundException(string trustName, Exception innerException) : base(Strings.ErrorNoAdministratorKeyFound(trustName), innerException)
		{
			this.trustName = trustName;
		}

		// Token: 0x0600B28C RID: 45708 RVA: 0x00299D46 File Offset: 0x00297F46
		protected NoAdministratorKeyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.trustName = (string)info.GetValue("trustName", typeof(string));
		}

		// Token: 0x0600B28D RID: 45709 RVA: 0x00299D70 File Offset: 0x00297F70
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("trustName", this.trustName);
		}

		// Token: 0x170038C3 RID: 14531
		// (get) Token: 0x0600B28E RID: 45710 RVA: 0x00299D8B File Offset: 0x00297F8B
		public string TrustName
		{
			get
			{
				return this.trustName;
			}
		}

		// Token: 0x04006229 RID: 25129
		private readonly string trustName;
	}
}
