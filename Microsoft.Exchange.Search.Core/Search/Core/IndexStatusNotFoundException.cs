using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C4 RID: 196
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IndexStatusNotFoundException : IndexStatusException
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00012F61 File Offset: 0x00011161
		public IndexStatusNotFoundException(string database) : base(Strings.IndexStatusNotFound(database))
		{
			this.database = database;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00012F7B File Offset: 0x0001117B
		public IndexStatusNotFoundException(string database, Exception innerException) : base(Strings.IndexStatusNotFound(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00012F96 File Offset: 0x00011196
		protected IndexStatusNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00012FC0 File Offset: 0x000111C0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00012FDB File Offset: 0x000111DB
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x040002CB RID: 715
		private readonly string database;
	}
}
