using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020011A1 RID: 4513
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EwsNotSupportedException : LocalizedException
	{
		// Token: 0x0600B715 RID: 46869 RVA: 0x002A0DD5 File Offset: 0x0029EFD5
		public EwsNotSupportedException() : base(Strings.EwsNotSupportedException)
		{
		}

		// Token: 0x0600B716 RID: 46870 RVA: 0x002A0DE2 File Offset: 0x0029EFE2
		public EwsNotSupportedException(Exception innerException) : base(Strings.EwsNotSupportedException, innerException)
		{
		}

		// Token: 0x0600B717 RID: 46871 RVA: 0x002A0DF0 File Offset: 0x0029EFF0
		protected EwsNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B718 RID: 46872 RVA: 0x002A0DFA File Offset: 0x0029EFFA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
