using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	internal class DataSeekException : InvalidOperationException
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0001AA00 File Offset: 0x00018C00
		public DataSeekException() : base(Strings.SeekGeneralFailure)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001AA12 File Offset: 0x00018C12
		public DataSeekException(string message) : base(message)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001AA1B File Offset: 0x00018C1B
		public DataSeekException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001AA25 File Offset: 0x00018C25
		public DataSeekException(DataRow row, DataTableCursor cursor, string message) : base(message)
		{
			this.row = row;
			this.cursor = cursor;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001AA3C File Offset: 0x00018C3C
		protected DataSeekException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.row = (DataRow)info.GetValue("Row", typeof(DataRow));
				this.cursor = (DataTableCursor)info.GetValue("Cursor", typeof(DataTableCursor));
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001AA94 File Offset: 0x00018C94
		public DataRow Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001AA9C File Offset: 0x00018C9C
		public DataTableCursor Cursor
		{
			get
			{
				return this.cursor;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001AAA4 File Offset: 0x00018CA4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Row", this.Row, typeof(DataRow));
			info.AddValue("cursor", this.Cursor, typeof(DataTableCursor));
		}

		// Token: 0x04000325 RID: 805
		private readonly DataRow row;

		// Token: 0x04000326 RID: 806
		private readonly DataTableCursor cursor;
	}
}
