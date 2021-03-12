using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B6 RID: 4278
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoAccountNamespaceException : FederationException
	{
		// Token: 0x0600B27E RID: 45694 RVA: 0x00299C8E File Offset: 0x00297E8E
		public NoAccountNamespaceException() : base(Strings.ErrorNoAccountNamespace)
		{
		}

		// Token: 0x0600B27F RID: 45695 RVA: 0x00299C9B File Offset: 0x00297E9B
		public NoAccountNamespaceException(Exception innerException) : base(Strings.ErrorNoAccountNamespace, innerException)
		{
		}

		// Token: 0x0600B280 RID: 45696 RVA: 0x00299CA9 File Offset: 0x00297EA9
		protected NoAccountNamespaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B281 RID: 45697 RVA: 0x00299CB3 File Offset: 0x00297EB3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
