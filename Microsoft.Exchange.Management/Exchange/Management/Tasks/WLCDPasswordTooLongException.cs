using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E64 RID: 3684
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPasswordTooLongException : WLCDMemberException
	{
		// Token: 0x0600A6DE RID: 42718 RVA: 0x00287DB1 File Offset: 0x00285FB1
		public WLCDPasswordTooLongException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6DF RID: 42719 RVA: 0x00287DBA File Offset: 0x00285FBA
		public WLCDPasswordTooLongException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6E0 RID: 42720 RVA: 0x00287DC4 File Offset: 0x00285FC4
		protected WLCDPasswordTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6E1 RID: 42721 RVA: 0x00287DCE File Offset: 0x00285FCE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
