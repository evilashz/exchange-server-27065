using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F2F RID: 3887
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToRemoveDatabaseSectionException : LocalizedException
	{
		// Token: 0x0600AAE0 RID: 43744 RVA: 0x0028E268 File Offset: 0x0028C468
		public FailedToRemoveDatabaseSectionException(string database) : base(Strings.FailedToRemoveDatabaseSection(database))
		{
			this.database = database;
		}

		// Token: 0x0600AAE1 RID: 43745 RVA: 0x0028E27D File Offset: 0x0028C47D
		public FailedToRemoveDatabaseSectionException(string database, Exception innerException) : base(Strings.FailedToRemoveDatabaseSection(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x0600AAE2 RID: 43746 RVA: 0x0028E293 File Offset: 0x0028C493
		protected FailedToRemoveDatabaseSectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x0600AAE3 RID: 43747 RVA: 0x0028E2BD File Offset: 0x0028C4BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x17003741 RID: 14145
		// (get) Token: 0x0600AAE4 RID: 43748 RVA: 0x0028E2D8 File Offset: 0x0028C4D8
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x040060A7 RID: 24743
		private readonly string database;
	}
}
