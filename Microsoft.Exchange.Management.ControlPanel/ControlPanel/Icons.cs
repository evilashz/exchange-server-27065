using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005F3 RID: 1523
	internal static class Icons
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x000CE5DB File Offset: 0x000CC7DB
		public static string FromEnum(RecipientTypeDetails recipientType)
		{
			return Icons.FromEnum(recipientType, Guid.Empty, false);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x000CE5EC File Offset: 0x000CC7EC
		public static string FromEnum(RecipientTypeDetails recipientType, Guid archiveGuid, bool isUserFederated)
		{
			string result = string.Empty;
			if (!archiveGuid.Equals(Guid.Empty))
			{
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.Archive16);
			}
			else
			{
				if (recipientType > RecipientTypeDetails.MailUniversalSecurityGroup)
				{
					if (recipientType <= (RecipientTypeDetails)((ulong)-2147483648))
					{
						if (recipientType <= RecipientTypeDetails.PublicFolder)
						{
							if (recipientType == RecipientTypeDetails.DynamicDistributionGroup)
							{
								return CommandSprite.GetCssClass(CommandSprite.SpriteId.DynamicDistributionGroup);
							}
							if (recipientType != RecipientTypeDetails.PublicFolder)
							{
								return result;
							}
							return CommandSprite.GetCssClass(CommandSprite.SpriteId.MailEnabledPublicFolder);
						}
						else
						{
							if (recipientType == RecipientTypeDetails.MailForestContact)
							{
								goto IL_1DE;
							}
							if (recipientType != (RecipientTypeDetails)((ulong)-2147483648))
							{
								return result;
							}
						}
					}
					else if (recipientType <= RecipientTypeDetails.RemoteEquipmentMailbox)
					{
						if (recipientType != RecipientTypeDetails.RemoteRoomMailbox && recipientType != RecipientTypeDetails.RemoteEquipmentMailbox)
						{
							return result;
						}
					}
					else if (recipientType != RecipientTypeDetails.RemoteSharedMailbox)
					{
						if (recipientType == RecipientTypeDetails.TeamMailbox)
						{
							goto IL_1A6;
						}
						if (recipientType != RecipientTypeDetails.RemoteTeamMailbox)
						{
							return result;
						}
					}
					return CommandSprite.GetCssClass(CommandSprite.SpriteId.RemoteMailbox16);
				}
				if (recipientType <= RecipientTypeDetails.EquipmentMailbox)
				{
					if (recipientType <= RecipientTypeDetails.LegacyMailbox)
					{
						if (recipientType <= RecipientTypeDetails.SharedMailbox)
						{
							if (recipientType < RecipientTypeDetails.UserMailbox)
							{
								return result;
							}
							switch ((int)(recipientType - RecipientTypeDetails.UserMailbox))
							{
							case 0:
								return CommandSprite.GetCssClass(isUserFederated ? CommandSprite.SpriteId.Federated16 : CommandSprite.SpriteId.Mailbox16);
							case 1:
								return CommandSprite.GetCssClass(CommandSprite.SpriteId.Linked16);
							case 2:
								return result;
							case 3:
								goto IL_1A6;
							}
						}
						if (recipientType != RecipientTypeDetails.LegacyMailbox)
						{
							return result;
						}
						return CommandSprite.GetCssClass(CommandSprite.SpriteId.Legacy16);
					}
					else
					{
						if (recipientType == RecipientTypeDetails.RoomMailbox)
						{
							return CommandSprite.GetCssClass(CommandSprite.SpriteId.Room16);
						}
						if (recipientType != RecipientTypeDetails.EquipmentMailbox)
						{
							return result;
						}
						return CommandSprite.GetCssClass(CommandSprite.SpriteId.Equipment16);
					}
				}
				else if (recipientType <= RecipientTypeDetails.MailUser)
				{
					if (recipientType == RecipientTypeDetails.MailContact)
					{
						goto IL_1DE;
					}
					if (recipientType != RecipientTypeDetails.MailUser)
					{
						return result;
					}
					return CommandSprite.GetCssClass(CommandSprite.SpriteId.MailUser);
				}
				else
				{
					if (recipientType != RecipientTypeDetails.MailUniversalDistributionGroup && recipientType != RecipientTypeDetails.MailNonUniversalGroup && recipientType != RecipientTypeDetails.MailUniversalSecurityGroup)
					{
						return result;
					}
					return CommandSprite.GetCssClass(CommandSprite.SpriteId.DistributionGroup16);
				}
				IL_1A6:
				return CommandSprite.GetCssClass(CommandSprite.SpriteId.Shared16);
				IL_1DE:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.Contact);
			}
			return result;
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x000CE804 File Offset: 0x000CCA04
		public static string GenerateIconAltText(RecipientTypeDetails recipientType)
		{
			string result = string.Empty;
			if (recipientType > RecipientTypeDetails.MailContact)
			{
				if (recipientType <= RecipientTypeDetails.MailUniversalDistributionGroup)
				{
					if (recipientType == RecipientTypeDetails.MailUser)
					{
						return Strings.MailUserAltText;
					}
					if (recipientType != RecipientTypeDetails.MailUniversalDistributionGroup)
					{
						return result;
					}
				}
				else if (recipientType != RecipientTypeDetails.MailNonUniversalGroup && recipientType != RecipientTypeDetails.MailUniversalSecurityGroup)
				{
					if (recipientType != RecipientTypeDetails.MailForestContact)
					{
						return result;
					}
					goto IL_102;
				}
				return Strings.DistributionGroupAltText;
			}
			if (recipientType <= RecipientTypeDetails.LegacyMailbox)
			{
				if (recipientType <= RecipientTypeDetails.SharedMailbox)
				{
					if (recipientType < RecipientTypeDetails.UserMailbox)
					{
						return result;
					}
					switch ((int)(recipientType - RecipientTypeDetails.UserMailbox))
					{
					case 0:
						return Strings.MailboxAltText;
					case 1:
						return Strings.LinkedAltText;
					case 2:
						return result;
					case 3:
						return Strings.SharedAltText;
					}
				}
				if (recipientType != RecipientTypeDetails.LegacyMailbox)
				{
					return result;
				}
				return Strings.LegacyAltText;
			}
			else
			{
				if (recipientType == RecipientTypeDetails.RoomMailbox)
				{
					return Strings.RoomAltText;
				}
				if (recipientType == RecipientTypeDetails.EquipmentMailbox)
				{
					return Strings.EquipmentAltText;
				}
				if (recipientType != RecipientTypeDetails.MailContact)
				{
					return result;
				}
			}
			IL_102:
			result = Strings.MailContactAltText;
			return result;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x000CE92C File Offset: 0x000CCB2C
		public static string FromEnum(SecurityPrincipalType securityPrincipalType)
		{
			string result = string.Empty;
			switch (securityPrincipalType)
			{
			case SecurityPrincipalType.User:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.Mailbox16);
				break;
			case SecurityPrincipalType.Group:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.DistributionGroup16);
				break;
			}
			return result;
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x000CE96C File Offset: 0x000CCB6C
		public static string GenerateIconAltText(SecurityPrincipalType securityPrincipalType)
		{
			string result = string.Empty;
			switch (securityPrincipalType)
			{
			case SecurityPrincipalType.User:
				result = Strings.MailboxAltText;
				break;
			case SecurityPrincipalType.Group:
				result = Strings.DistributionGroupAltText;
				break;
			}
			return result;
		}

		// Token: 0x04002DE5 RID: 11749
		public const string Warning = "Warning.gif";
	}
}
