using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E45 RID: 3653
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WindowsLiveIdAlreadyUsedException : RecipientTaskException
	{
		// Token: 0x0600A65B RID: 42587 RVA: 0x002876BA File Offset: 0x002858BA
		public WindowsLiveIdAlreadyUsedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A65C RID: 42588 RVA: 0x002876C3 File Offset: 0x002858C3
		public WindowsLiveIdAlreadyUsedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A65D RID: 42589 RVA: 0x002876CD File Offset: 0x002858CD
		protected WindowsLiveIdAlreadyUsedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A65E RID: 42590 RVA: 0x002876D7 File Offset: 0x002858D7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
