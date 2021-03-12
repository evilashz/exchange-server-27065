using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000100 RID: 256
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CantFindCalendarFolderException : StoragePermanentException
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x000696BE File Offset: 0x000678BE
		public CantFindCalendarFolderException(object identity) : base(ServerStrings.CantFindCalendarFolderException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000696D3 File Offset: 0x000678D3
		public CantFindCalendarFolderException(object identity, Exception innerException) : base(ServerStrings.CantFindCalendarFolderException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000696E9 File Offset: 0x000678E9
		protected CantFindCalendarFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = info.GetValue("identity", typeof(object));
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0006970E File Offset: 0x0006790E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00069729 File Offset: 0x00067929
		public object Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000991 RID: 2449
		private readonly object identity;
	}
}
