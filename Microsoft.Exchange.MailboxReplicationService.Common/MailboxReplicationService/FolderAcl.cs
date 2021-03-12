using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000015 RID: 21
	[DataContract]
	internal class FolderAcl : ItemPropertiesBase
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00003A50 File Offset: 0x00001C50
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00003A58 File Offset: 0x00001C58
		[DataMember(Name = "Flags")]
		public int FlagsInt { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00003A61 File Offset: 0x00001C61
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00003A69 File Offset: 0x00001C69
		[DataMember(Name = "Value")]
		public PropValueData[][] Value { get; set; }

		// Token: 0x06000179 RID: 377 RVA: 0x00003A72 File Offset: 0x00001C72
		public FolderAcl(AclFlags flags, PropValueData[][] pvda)
		{
			this.Flags = flags;
			this.Value = pvda;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003A88 File Offset: 0x00001C88
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00003A90 File Offset: 0x00001C90
		public AclFlags Flags
		{
			get
			{
				return (AclFlags)this.FlagsInt;
			}
			set
			{
				this.FlagsInt = (int)value;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003A9C File Offset: 0x00001C9C
		public override void Apply(CoreFolder folder)
		{
			if (this.Value == null)
			{
				return;
			}
			ModifyTableOptions modifyTableOptions = this.Flags.HasFlag(AclFlags.FreeBusyAcl) ? ModifyTableOptions.FreeBusyAware : ModifyTableOptions.None;
			modifyTableOptions |= ModifyTableOptions.ExtendedPermissionInformation;
			using (IModifyTable permissionTableDoNotLoadEntries = folder.GetPermissionTableDoNotLoadEntries(modifyTableOptions))
			{
				foreach (PropValueData[] array in this.Value)
				{
					List<PropValue> list = new List<PropValue>();
					int j = 0;
					while (j < array.Length)
					{
						PropValueData propValueData = array[j];
						int propTag = propValueData.PropTag;
						if (propTag <= 1718747166)
						{
							if (propTag != 268370178)
							{
								if (propTag != 1718681620)
								{
									if (propTag != 1718747166)
									{
										goto IL_168;
									}
									list.Add(new PropValue(PermissionSchema.MemberName, (string)propValueData.Value));
								}
							}
							else
							{
								byte[] array2 = (byte[])propValueData.Value;
								if (array2 != null)
								{
									list.Add(new PropValue(PermissionSchema.MemberEntryId, array2));
								}
							}
						}
						else if (propTag != 1718812675)
						{
							if (propTag != 1718878466)
							{
								if (propTag != 1718943755)
								{
									goto IL_168;
								}
								list.Add(new PropValue(PermissionSchema.MemberIsGroup, (bool)propValueData.Value));
							}
							else
							{
								list.Add(new PropValue(PermissionSchema.MemberSecurityIdentifier, (byte[])propValueData.Value));
							}
						}
						else
						{
							list.Add(new PropValue(PermissionSchema.MemberRights, (MemberRights)propValueData.Value));
						}
						IL_191:
						j++;
						continue;
						IL_168:
						MrsTracer.Provider.Warning("StorageDestinationFolder.SetAcl: Unknown PropTag 0x{0:x}", new object[]
						{
							propValueData.PropTag
						});
						goto IL_191;
					}
					permissionTableDoNotLoadEntries.AddRow(list.ToArray());
				}
				permissionTableDoNotLoadEntries.ApplyPendingChanges();
			}
		}
	}
}
