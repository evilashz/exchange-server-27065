using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000E87 RID: 3719
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorResourceRoomOrEquipmentOnlyException : LocalizedException
	{
		// Token: 0x0600A775 RID: 42869 RVA: 0x0028869C File Offset: 0x0028689C
		public ErrorResourceRoomOrEquipmentOnlyException(string room, string equipment, string fullString, string partialType) : base(Strings.ErrorResourceRoomOrEquipmentOnly(room, equipment, fullString, partialType))
		{
			this.room = room;
			this.equipment = equipment;
			this.fullString = fullString;
			this.partialType = partialType;
		}

		// Token: 0x0600A776 RID: 42870 RVA: 0x002886CB File Offset: 0x002868CB
		public ErrorResourceRoomOrEquipmentOnlyException(string room, string equipment, string fullString, string partialType, Exception innerException) : base(Strings.ErrorResourceRoomOrEquipmentOnly(room, equipment, fullString, partialType), innerException)
		{
			this.room = room;
			this.equipment = equipment;
			this.fullString = fullString;
			this.partialType = partialType;
		}

		// Token: 0x0600A777 RID: 42871 RVA: 0x002886FC File Offset: 0x002868FC
		protected ErrorResourceRoomOrEquipmentOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.room = (string)info.GetValue("room", typeof(string));
			this.equipment = (string)info.GetValue("equipment", typeof(string));
			this.fullString = (string)info.GetValue("fullString", typeof(string));
			this.partialType = (string)info.GetValue("partialType", typeof(string));
		}

		// Token: 0x0600A778 RID: 42872 RVA: 0x00288794 File Offset: 0x00286994
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("room", this.room);
			info.AddValue("equipment", this.equipment);
			info.AddValue("fullString", this.fullString);
			info.AddValue("partialType", this.partialType);
		}

		// Token: 0x17003676 RID: 13942
		// (get) Token: 0x0600A779 RID: 42873 RVA: 0x002887ED File Offset: 0x002869ED
		public string Room
		{
			get
			{
				return this.room;
			}
		}

		// Token: 0x17003677 RID: 13943
		// (get) Token: 0x0600A77A RID: 42874 RVA: 0x002887F5 File Offset: 0x002869F5
		public string Equipment
		{
			get
			{
				return this.equipment;
			}
		}

		// Token: 0x17003678 RID: 13944
		// (get) Token: 0x0600A77B RID: 42875 RVA: 0x002887FD File Offset: 0x002869FD
		public string FullString
		{
			get
			{
				return this.fullString;
			}
		}

		// Token: 0x17003679 RID: 13945
		// (get) Token: 0x0600A77C RID: 42876 RVA: 0x00288805 File Offset: 0x00286A05
		public string PartialType
		{
			get
			{
				return this.partialType;
			}
		}

		// Token: 0x04005FDC RID: 24540
		private readonly string room;

		// Token: 0x04005FDD RID: 24541
		private readonly string equipment;

		// Token: 0x04005FDE RID: 24542
		private readonly string fullString;

		// Token: 0x04005FDF RID: 24543
		private readonly string partialType;
	}
}
