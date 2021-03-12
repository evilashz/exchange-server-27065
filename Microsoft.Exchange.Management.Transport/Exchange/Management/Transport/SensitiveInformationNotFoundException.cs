using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000191 RID: 401
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SensitiveInformationNotFoundException : SensitiveInformationCmdletException
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x000368C1 File Offset: 0x00034AC1
		public SensitiveInformationNotFoundException(string identity) : base(Strings.SensitiveInformationNotFound(identity))
		{
			this.identity = identity;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000368D6 File Offset: 0x00034AD6
		public SensitiveInformationNotFoundException(string identity, Exception innerException) : base(Strings.SensitiveInformationNotFound(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000368EC File Offset: 0x00034AEC
		protected SensitiveInformationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00036916 File Offset: 0x00034B16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00036931 File Offset: 0x00034B31
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000687 RID: 1671
		private readonly string identity;
	}
}
