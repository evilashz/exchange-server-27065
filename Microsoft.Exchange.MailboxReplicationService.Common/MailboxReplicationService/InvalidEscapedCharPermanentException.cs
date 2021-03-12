using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C6 RID: 710
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEscapedCharPermanentException : FolderFilterPermanentException
	{
		// Token: 0x060023A2 RID: 9122 RVA: 0x0004ED8D File Offset: 0x0004CF8D
		public InvalidEscapedCharPermanentException(string folderPath, int charPosition) : base(MrsStrings.InvalidEscapedChar(folderPath, charPosition))
		{
			this.folderPath = folderPath;
			this.charPosition = charPosition;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0004EDAA File Offset: 0x0004CFAA
		public InvalidEscapedCharPermanentException(string folderPath, int charPosition, Exception innerException) : base(MrsStrings.InvalidEscapedChar(folderPath, charPosition), innerException)
		{
			this.folderPath = folderPath;
			this.charPosition = charPosition;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0004EDC8 File Offset: 0x0004CFC8
		protected InvalidEscapedCharPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderPath = (string)info.GetValue("folderPath", typeof(string));
			this.charPosition = (int)info.GetValue("charPosition", typeof(int));
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0004EE1D File Offset: 0x0004D01D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderPath", this.folderPath);
			info.AddValue("charPosition", this.charPosition);
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0004EE49 File Offset: 0x0004D049
		public string FolderPath
		{
			get
			{
				return this.folderPath;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x0004EE51 File Offset: 0x0004D051
		public int CharPosition
		{
			get
			{
				return this.charPosition;
			}
		}

		// Token: 0x04000FD3 RID: 4051
		private readonly string folderPath;

		// Token: 0x04000FD4 RID: 4052
		private readonly int charPosition;
	}
}
