using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x02000043 RID: 67
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class GroupAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000086DF File Offset: 0x000068DF
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000086E7 File Offset: 0x000068E7
		[XmlElement]
		public int NumberOfMembers
		{
			get
			{
				return this.numberOfMembers;
			}
			set
			{
				this.numberOfMembers = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000086F0 File Offset: 0x000068F0
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000086F8 File Offset: 0x000068F8
		[XmlElement]
		public int NumberOfMembersAvailable
		{
			get
			{
				return this.numberOfMembersAvailable;
			}
			set
			{
				this.numberOfMembersAvailable = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008701 File Offset: 0x00006901
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00008709 File Offset: 0x00006909
		[XmlElement]
		public int NumberOfMembersWithConflict
		{
			get
			{
				return this.numberOfMembersWithConflict;
			}
			set
			{
				this.numberOfMembersWithConflict = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008712 File Offset: 0x00006912
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000871A File Offset: 0x0000691A
		[XmlElement]
		public int NumberOfMembersWithNoData
		{
			get
			{
				return this.numberOfMembersWithNoData;
			}
			set
			{
				this.numberOfMembersWithNoData = value;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008724 File Offset: 0x00006924
		internal static GroupAttendeeConflictData Create(int numberOfMembers, int numberOfMembersWithConflict, int numberOfMembersAvailable, int numberOfMembersWithNoData)
		{
			return new GroupAttendeeConflictData
			{
				numberOfMembers = numberOfMembers,
				numberOfMembersWithConflict = numberOfMembersWithConflict,
				numberOfMembersAvailable = numberOfMembersAvailable,
				numberOfMembersWithNoData = numberOfMembersWithNoData
			};
		}

		// Token: 0x040000E9 RID: 233
		private int numberOfMembers;

		// Token: 0x040000EA RID: 234
		private int numberOfMembersWithConflict;

		// Token: 0x040000EB RID: 235
		private int numberOfMembersAvailable;

		// Token: 0x040000EC RID: 236
		private int numberOfMembersWithNoData;
	}
}
