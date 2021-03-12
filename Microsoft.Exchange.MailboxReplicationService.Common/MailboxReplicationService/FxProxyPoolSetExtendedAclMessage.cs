using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000256 RID: 598
	internal class FxProxyPoolSetExtendedAclMessage : DataMessageBase
	{
		// Token: 0x06001EAE RID: 7854 RVA: 0x0003F9D2 File Offset: 0x0003DBD2
		public FxProxyPoolSetExtendedAclMessage(AclFlags aclFlags, PropValueData[][] aclData)
		{
			this.aclFlags = aclFlags;
			this.aclData = aclData;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		public FxProxyPoolSetExtendedAclMessage(byte[] blob)
		{
			FxProxyPoolSetExtendedAclMessage.AclRec aclRec = CommonUtils.DataContractDeserialize<FxProxyPoolSetExtendedAclMessage.AclRec>(blob);
			if (aclRec != null)
			{
				this.aclFlags = aclRec.AclFlags;
				this.aclData = aclRec.AclData;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0003FA20 File Offset: 0x0003DC20
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolSetExtendedAcl
				};
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0003FA3A File Offset: 0x0003DC3A
		public AclFlags AclFlags
		{
			get
			{
				return this.aclFlags;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0003FA42 File Offset: 0x0003DC42
		public PropValueData[][] AclData
		{
			get
			{
				return this.aclData;
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0003FA4A File Offset: 0x0003DC4A
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolSetExtendedAclMessage(data);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0003FA54 File Offset: 0x0003DC54
		protected override int GetSizeInternal()
		{
			int num = 4;
			if (this.AclData != null)
			{
				foreach (PropValueData[] array2 in this.AclData)
				{
					foreach (PropValueData propValueData in array2)
					{
						num += propValueData.GetApproximateSize();
					}
				}
			}
			return num;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0003FAB0 File Offset: 0x0003DCB0
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolSetExtendedAcl;
			data = CommonUtils.DataContractSerialize<FxProxyPoolSetExtendedAclMessage.AclRec>(new FxProxyPoolSetExtendedAclMessage.AclRec
			{
				AclFlags = this.aclFlags,
				AclData = this.AclData
			});
		}

		// Token: 0x04000C71 RID: 3185
		private readonly AclFlags aclFlags;

		// Token: 0x04000C72 RID: 3186
		private readonly PropValueData[][] aclData;

		// Token: 0x02000257 RID: 599
		[DataContract]
		internal sealed class AclRec
		{
			// Token: 0x17000BC2 RID: 3010
			// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0003FAEF File Offset: 0x0003DCEF
			// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x0003FAF7 File Offset: 0x0003DCF7
			[DataMember(IsRequired = true)]
			public AclFlags AclFlags { get; set; }

			// Token: 0x17000BC3 RID: 3011
			// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x0003FB00 File Offset: 0x0003DD00
			// (set) Token: 0x06001EBA RID: 7866 RVA: 0x0003FB08 File Offset: 0x0003DD08
			[DataMember(IsRequired = true)]
			public PropValueData[][] AclData { get; set; }
		}
	}
}
