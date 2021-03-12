using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F04 RID: 3844
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MdbServerNotFoundException : LocalizedException
	{
		// Token: 0x0600A9FD RID: 43517 RVA: 0x0028C9FA File Offset: 0x0028ABFA
		public MdbServerNotFoundException(string database) : base(Strings.MdbServerNotFoundException(database))
		{
			this.database = database;
		}

		// Token: 0x0600A9FE RID: 43518 RVA: 0x0028CA0F File Offset: 0x0028AC0F
		public MdbServerNotFoundException(string database, Exception innerException) : base(Strings.MdbServerNotFoundException(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x0600A9FF RID: 43519 RVA: 0x0028CA25 File Offset: 0x0028AC25
		protected MdbServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x0600AA00 RID: 43520 RVA: 0x0028CA4F File Offset: 0x0028AC4F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x1700370A RID: 14090
		// (get) Token: 0x0600AA01 RID: 43521 RVA: 0x0028CA6A File Offset: 0x0028AC6A
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x04006070 RID: 24688
		private readonly string database;
	}
}
