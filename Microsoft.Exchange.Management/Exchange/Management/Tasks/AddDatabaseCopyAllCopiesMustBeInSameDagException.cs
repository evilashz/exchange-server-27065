using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE8 RID: 3816
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddDatabaseCopyAllCopiesMustBeInSameDagException : LocalizedException
	{
		// Token: 0x0600A967 RID: 43367 RVA: 0x0028B971 File Offset: 0x00289B71
		public AddDatabaseCopyAllCopiesMustBeInSameDagException(string databaseName, string server1, string dag1, string server2, string dag2) : base(Strings.AddDatabaseCopyAllCopiesMustBeInSameDagException(databaseName, server1, dag1, server2, dag2))
		{
			this.databaseName = databaseName;
			this.server1 = server1;
			this.dag1 = dag1;
			this.server2 = server2;
			this.dag2 = dag2;
		}

		// Token: 0x0600A968 RID: 43368 RVA: 0x0028B9AA File Offset: 0x00289BAA
		public AddDatabaseCopyAllCopiesMustBeInSameDagException(string databaseName, string server1, string dag1, string server2, string dag2, Exception innerException) : base(Strings.AddDatabaseCopyAllCopiesMustBeInSameDagException(databaseName, server1, dag1, server2, dag2), innerException)
		{
			this.databaseName = databaseName;
			this.server1 = server1;
			this.dag1 = dag1;
			this.server2 = server2;
			this.dag2 = dag2;
		}

		// Token: 0x0600A969 RID: 43369 RVA: 0x0028B9E8 File Offset: 0x00289BE8
		protected AddDatabaseCopyAllCopiesMustBeInSameDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.server1 = (string)info.GetValue("server1", typeof(string));
			this.dag1 = (string)info.GetValue("dag1", typeof(string));
			this.server2 = (string)info.GetValue("server2", typeof(string));
			this.dag2 = (string)info.GetValue("dag2", typeof(string));
		}

		// Token: 0x0600A96A RID: 43370 RVA: 0x0028BAA0 File Offset: 0x00289CA0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("server1", this.server1);
			info.AddValue("dag1", this.dag1);
			info.AddValue("server2", this.server2);
			info.AddValue("dag2", this.dag2);
		}

		// Token: 0x170036E4 RID: 14052
		// (get) Token: 0x0600A96B RID: 43371 RVA: 0x0028BB0A File Offset: 0x00289D0A
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x170036E5 RID: 14053
		// (get) Token: 0x0600A96C RID: 43372 RVA: 0x0028BB12 File Offset: 0x00289D12
		public string Server1
		{
			get
			{
				return this.server1;
			}
		}

		// Token: 0x170036E6 RID: 14054
		// (get) Token: 0x0600A96D RID: 43373 RVA: 0x0028BB1A File Offset: 0x00289D1A
		public string Dag1
		{
			get
			{
				return this.dag1;
			}
		}

		// Token: 0x170036E7 RID: 14055
		// (get) Token: 0x0600A96E RID: 43374 RVA: 0x0028BB22 File Offset: 0x00289D22
		public string Server2
		{
			get
			{
				return this.server2;
			}
		}

		// Token: 0x170036E8 RID: 14056
		// (get) Token: 0x0600A96F RID: 43375 RVA: 0x0028BB2A File Offset: 0x00289D2A
		public string Dag2
		{
			get
			{
				return this.dag2;
			}
		}

		// Token: 0x0400604A RID: 24650
		private readonly string databaseName;

		// Token: 0x0400604B RID: 24651
		private readonly string server1;

		// Token: 0x0400604C RID: 24652
		private readonly string dag1;

		// Token: 0x0400604D RID: 24653
		private readonly string server2;

		// Token: 0x0400604E RID: 24654
		private readonly string dag2;
	}
}
