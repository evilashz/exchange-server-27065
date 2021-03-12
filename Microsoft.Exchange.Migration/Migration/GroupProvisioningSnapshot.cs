using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupProvisioningSnapshot : ProvisioningSnapshot, IMigrationSerializable
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00009572 File Offset: 0x00007772
		public GroupProvisioningSnapshot(ProvisionedObject provisionedObject) : base(provisionedObject)
		{
			this.CountOfProvisionedMembers = provisionedObject.GroupMemberProvisioned;
			this.CountOfSkippedMembers = provisionedObject.GroupMemberSkipped;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009595 File Offset: 0x00007795
		public GroupProvisioningSnapshot()
		{
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000959D File Offset: 0x0000779D
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000095A5 File Offset: 0x000077A5
		public GroupMembershipProvisioningState ProvisioningState { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000095AE File Offset: 0x000077AE
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000095B6 File Offset: 0x000077B6
		public int CountOfProvisionedMembers { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000095BF File Offset: 0x000077BF
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000095C7 File Offset: 0x000077C7
		public int CountOfSkippedMembers { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000095D0 File Offset: 0x000077D0
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return GroupProvisioningSnapshot.GroupPropertyDefinitions;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000095D8 File Offset: 0x000077D8
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState] = (int)this.ProvisioningState;
			message[MigrationBatchMessageSchema.MigrationJobItemGroupMemberSkipped] = this.CountOfSkippedMembers;
			message[MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioned] = this.CountOfProvisionedMembers;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009628 File Offset: 0x00007828
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			int valueOrDefault = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState, 0);
			if (!Enum.IsDefined(typeof(GroupMembershipProvisioningState), valueOrDefault))
			{
				throw new MigrationDataCorruptionException("Invalid MigrationJobItemGroupMemberProvisioningState. Message ID: " + message.Id);
			}
			this.ProvisioningState = (GroupMembershipProvisioningState)valueOrDefault;
			this.CountOfSkippedMembers = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemGroupMemberSkipped, 0);
			this.CountOfProvisionedMembers = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioned, 0);
			return true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000969C File Offset: 0x0000789C
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("GroupProvisioningSnapshot");
			xelement.Add(new object[]
			{
				new XElement("ProvisioningState", this.ProvisioningState),
				new XElement("MembersProvisioned", this.CountOfProvisionedMembers),
				new XElement("MembersSkipped", this.CountOfSkippedMembers)
			});
			return xelement;
		}

		// Token: 0x040000AE RID: 174
		public static readonly PropertyDefinition[] GroupPropertyDefinitions = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioned,
			MigrationBatchMessageSchema.MigrationJobItemGroupMemberSkipped,
			MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState
		};
	}
}
