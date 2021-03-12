using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	internal class SchemaException : SystemException
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0001EC12 File Offset: 0x0001CE12
		public SchemaException() : base(Strings.SchemaInvalid)
		{
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001EC24 File Offset: 0x0001CE24
		public SchemaException(string message) : base(message)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001EC2D File Offset: 0x0001CE2D
		public SchemaException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001EC37 File Offset: 0x0001CE37
		public SchemaException(string message, JET_TABLEID table, DataColumn column) : base(message)
		{
			this.table = table;
			this.column = column;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001EC50 File Offset: 0x0001CE50
		protected SchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.table = (JET_TABLEID)info.GetValue("Table", typeof(JET_TABLEID));
				this.column = (DataColumn)info.GetValue("Column", typeof(DataColumn));
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		public DataColumn Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		public JET_TABLEID Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Column", this.Column, typeof(DataColumn));
			info.AddValue("Table", this.Table, typeof(JET_TABLEID));
		}

		// Token: 0x040003DA RID: 986
		private DataColumn column;

		// Token: 0x040003DB RID: 987
		private JET_TABLEID table;
	}
}
