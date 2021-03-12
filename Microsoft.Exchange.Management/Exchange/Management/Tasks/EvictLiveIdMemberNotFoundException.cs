using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E54 RID: 3668
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EvictLiveIdMemberNotFoundException : RecipientTaskException
	{
		// Token: 0x0600A69E RID: 42654 RVA: 0x00287B41 File Offset: 0x00285D41
		public EvictLiveIdMemberNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A69F RID: 42655 RVA: 0x00287B4A File Offset: 0x00285D4A
		public EvictLiveIdMemberNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6A0 RID: 42656 RVA: 0x00287B54 File Offset: 0x00285D54
		protected EvictLiveIdMemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6A1 RID: 42657 RVA: 0x00287B5E File Offset: 0x00285D5E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
