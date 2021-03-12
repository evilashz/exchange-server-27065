using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000006 RID: 6
	internal class CountedEntity<TEntityType> : ICountedEntity<TEntityType>, IEquatable<ICountedEntity<TEntityType>> where TEntityType : struct, IConvertible
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002EE2 File Offset: 0x000010E2
		public CountedEntity(IEntityName<TEntityType> groupName, IEntityName<TEntityType> name)
		{
			ArgumentValidator.ThrowIfNull("groupName", groupName);
			ArgumentValidator.ThrowIfNull("name", name);
			this.GroupName = groupName;
			this.Name = name;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002F0E File Offset: 0x0000110E
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002F16 File Offset: 0x00001116
		public IEntityName<TEntityType> GroupName { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F1F File Offset: 0x0000111F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002F27 File Offset: 0x00001127
		public IEntityName<TEntityType> Name { get; private set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002F30 File Offset: 0x00001130
		public bool Equals(ICountedEntity<TEntityType> other)
		{
			return other != null && this.GroupName.Equals(other.GroupName) && this.Name.Equals(other.Name);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F5D File Offset: 0x0000115D
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is ICountedEntity<TEntityType> && this.Equals(obj as ICountedEntity<TEntityType>)));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F8B File Offset: 0x0000118B
		public override int GetHashCode()
		{
			return this.GroupName.GetHashCode() * 397 ^ this.Name.GetHashCode();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FAA File Offset: 0x000011AA
		public override string ToString()
		{
			return string.Format("Group:{0};Name:{1}", this.GroupName, this.Name);
		}
	}
}
