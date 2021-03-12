using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F2 RID: 242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSharingDataException : StoragePermanentException
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x00069214 File Offset: 0x00067414
		public InvalidSharingDataException(string name, string value) : base(ServerStrings.InvalidSharingDataException(name, value))
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00069231 File Offset: 0x00067431
		public InvalidSharingDataException(string name, string value, Exception innerException) : base(ServerStrings.InvalidSharingDataException(name, value), innerException)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00069250 File Offset: 0x00067450
		protected InvalidSharingDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000692A5 File Offset: 0x000674A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x000692D1 File Offset: 0x000674D1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x000692D9 File Offset: 0x000674D9
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400098A RID: 2442
		private readonly string name;

		// Token: 0x0400098B RID: 2443
		private readonly string value;
	}
}
