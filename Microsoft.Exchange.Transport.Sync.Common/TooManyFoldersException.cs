using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200003B RID: 59
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TooManyFoldersException : IMAPException
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00005BC5 File Offset: 0x00003DC5
		public TooManyFoldersException(int maxNumber) : base(Strings.TooManyFoldersException(maxNumber))
		{
			this.maxNumber = maxNumber;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005BDF File Offset: 0x00003DDF
		public TooManyFoldersException(int maxNumber, Exception innerException) : base(Strings.TooManyFoldersException(maxNumber), innerException)
		{
			this.maxNumber = maxNumber;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005BFA File Offset: 0x00003DFA
		protected TooManyFoldersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maxNumber = (int)info.GetValue("maxNumber", typeof(int));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005C24 File Offset: 0x00003E24
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maxNumber", this.maxNumber);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005C3F File Offset: 0x00003E3F
		public int MaxNumber
		{
			get
			{
				return this.maxNumber;
			}
		}

		// Token: 0x040000ED RID: 237
		private readonly int maxNumber;
	}
}
