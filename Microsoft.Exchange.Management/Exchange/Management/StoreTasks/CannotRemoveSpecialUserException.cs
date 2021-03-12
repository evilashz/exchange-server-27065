using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020010FD RID: 4349
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveSpecialUserException : StoragePermanentException
	{
		// Token: 0x0600B3DA RID: 46042 RVA: 0x0029BCDE File Offset: 0x00299EDE
		public CannotRemoveSpecialUserException() : base(Strings.ErrorCannotRemoveSpecialUser)
		{
		}

		// Token: 0x0600B3DB RID: 46043 RVA: 0x0029BCEB File Offset: 0x00299EEB
		public CannotRemoveSpecialUserException(Exception innerException) : base(Strings.ErrorCannotRemoveSpecialUser, innerException)
		{
		}

		// Token: 0x0600B3DC RID: 46044 RVA: 0x0029BCF9 File Offset: 0x00299EF9
		protected CannotRemoveSpecialUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B3DD RID: 46045 RVA: 0x0029BD03 File Offset: 0x00299F03
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
