using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002AA RID: 682
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnexpectedValuePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600230E RID: 8974 RVA: 0x0004DD6D File Offset: 0x0004BF6D
		public UnexpectedValuePermanentException(string value, string parameterName) : base(MrsStrings.UnexpectedValue(value, parameterName))
		{
			this.value = value;
			this.parameterName = parameterName;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0004DD8A File Offset: 0x0004BF8A
		public UnexpectedValuePermanentException(string value, string parameterName, Exception innerException) : base(MrsStrings.UnexpectedValue(value, parameterName), innerException)
		{
			this.value = value;
			this.parameterName = parameterName;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0004DDA8 File Offset: 0x0004BFA8
		protected UnexpectedValuePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0004DDFD File Offset: 0x0004BFFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
			info.AddValue("parameterName", this.parameterName);
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0004DE29 File Offset: 0x0004C029
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x0004DE31 File Offset: 0x0004C031
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x04000FAF RID: 4015
		private readonly string value;

		// Token: 0x04000FB0 RID: 4016
		private readonly string parameterName;
	}
}
