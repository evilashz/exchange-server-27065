using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F0 RID: 240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSharingMessageException : StoragePermanentException
	{
		// Token: 0x06001342 RID: 4930 RVA: 0x0006916D File Offset: 0x0006736D
		public InvalidSharingMessageException(string property) : base(ServerStrings.InvalidSharingMessageException(property))
		{
			this.property = property;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00069182 File Offset: 0x00067382
		public InvalidSharingMessageException(string property, Exception innerException) : base(ServerStrings.InvalidSharingMessageException(property), innerException)
		{
			this.property = property;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00069198 File Offset: 0x00067398
		protected InvalidSharingMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000691C2 File Offset: 0x000673C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x000691DD File Offset: 0x000673DD
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x04000989 RID: 2441
		private readonly string property;
	}
}
