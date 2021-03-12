using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005E RID: 94
	[DataContract]
	internal sealed class NamedPropData : IEquatable<NamedPropData>
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00008C54 File Offset: 0x00006E54
		public NamedPropData()
		{
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00008C5C File Offset: 0x00006E5C
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00008C64 File Offset: 0x00006E64
		[DataMember(IsRequired = true)]
		public int Kind { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00008C6D File Offset: 0x00006E6D
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00008C75 File Offset: 0x00006E75
		[DataMember(EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00008C7E File Offset: 0x00006E7E
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00008C86 File Offset: 0x00006E86
		[DataMember(EmitDefaultValue = false)]
		public int Id { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00008C8F File Offset: 0x00006E8F
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00008C97 File Offset: 0x00006E97
		[DataMember(EmitDefaultValue = false)]
		public Guid Guid { get; set; }

		// Token: 0x060004A9 RID: 1193 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public NamedPropData(Guid npGuid, string npName)
		{
			this.Guid = npGuid;
			this.Name = npName;
			this.Kind = 1;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00008CBD File Offset: 0x00006EBD
		public NamedPropData(Guid npGuid, int npId)
		{
			this.Guid = npGuid;
			this.Id = npId;
			this.Kind = 0;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00008CDC File Offset: 0x00006EDC
		public override int GetHashCode()
		{
			if (this.Kind == 0)
			{
				return this.Id.GetHashCode() ^ this.Guid.GetHashCode();
			}
			return this.Name.GetHashCode() ^ this.Guid.GetHashCode();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00008D38 File Offset: 0x00006F38
		public override string ToString()
		{
			return string.Format("[{0}:{1}]", (this.Kind == 0) ? ("0x" + this.Id.ToString("X")) : this.Name, this.Guid);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00008D88 File Offset: 0x00006F88
		public bool Equals(NamedPropData other)
		{
			if (this.Kind != other.Kind)
			{
				return false;
			}
			if (this.Kind == 0)
			{
				return this.Id == other.Id && this.Guid == other.Guid;
			}
			return this.Guid == other.Guid && string.Compare(this.Name, other.Name) == 0;
		}
	}
}
