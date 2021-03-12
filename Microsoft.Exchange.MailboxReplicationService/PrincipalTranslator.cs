using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000053 RID: 83
	internal class PrincipalTranslator
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0001964C File Offset: 0x0001784C
		public PrincipalTranslator(PrincipalMapper sourceMapper, PrincipalMapper targetMapper)
		{
			this.sourceMapper = sourceMapper;
			this.targetMapper = targetMapper;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00019662 File Offset: 0x00017862
		public void Clear()
		{
			this.sourceMapper.Clear();
			this.targetMapper.Clear();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001967C File Offset: 0x0001787C
		public void EnumerateSecurityDescriptor(RawSecurityDescriptor sd)
		{
			if (sd == null)
			{
				return;
			}
			this.sourceMapper.AddSid(sd.Owner);
			if (sd.DiscretionaryAcl != null)
			{
				foreach (GenericAce genericAce in sd.DiscretionaryAcl)
				{
					KnownAce knownAce = genericAce as KnownAce;
					if (knownAce != null)
					{
						this.sourceMapper.AddSid(knownAce.SecurityIdentifier);
					}
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000196E4 File Offset: 0x000178E4
		public void EnumerateFolderACL(PropValueData[][] folderACL)
		{
			if (folderACL == null)
			{
				return;
			}
			foreach (PropValueData[] array in folderACL)
			{
				if (array != null)
				{
					foreach (PropValueData pvd in array)
					{
						this.EnumeratePropValue(pvd);
					}
				}
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00019730 File Offset: 0x00017930
		public void EnumerateRules(RuleData[] rules)
		{
			if (rules != null)
			{
				foreach (RuleData ruleData in rules)
				{
					ruleData.Enumerate(null, new CommonUtils.EnumPropValueDelegate(this.EnumeratePropValue), new CommonUtils.EnumAdrEntryDelegate(this.EnumerateAdrEntry));
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00019774 File Offset: 0x00017974
		public RawSecurityDescriptor TranslateSecurityDescriptor(RawSecurityDescriptor sourceSD, TranslateSecurityDescriptorFlags flags)
		{
			if (sourceSD == null)
			{
				return null;
			}
			this.ResolveMappings();
			RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(sourceSD.ControlFlags, sourceSD.Owner, sourceSD.Group, sourceSD.SystemAcl, sourceSD.DiscretionaryAcl);
			bool flag = (flags & TranslateSecurityDescriptorFlags.ExcludeUnmappedACEs) != TranslateSecurityDescriptorFlags.None;
			SecurityIdentifier securityIdentifier;
			if (this.TryTranslateSid(sourceSD.Owner, out securityIdentifier))
			{
				rawSecurityDescriptor.Owner = securityIdentifier;
				MrsTracer.Service.Debug("Mapped SD owner from {0} to {1}", new object[]
				{
					sourceSD.Owner,
					rawSecurityDescriptor.Owner
				});
			}
			else if (flag)
			{
				rawSecurityDescriptor.Owner = null;
			}
			if (this.TryTranslateSid(sourceSD.Group, out securityIdentifier))
			{
				rawSecurityDescriptor.Group = securityIdentifier;
				MrsTracer.Service.Debug("Mapped SD group from {0} to {1}", new object[]
				{
					sourceSD.Group,
					rawSecurityDescriptor.Group
				});
			}
			else if (flag)
			{
				rawSecurityDescriptor.Group = null;
			}
			if (sourceSD.DiscretionaryAcl != null)
			{
				for (int i = sourceSD.DiscretionaryAcl.Count - 1; i >= 0; i--)
				{
					KnownAce knownAce = sourceSD.DiscretionaryAcl[i] as KnownAce;
					if (knownAce == null)
					{
						if (flag)
						{
							sourceSD.DiscretionaryAcl.RemoveAce(i);
						}
					}
					else if (this.TryTranslateSid(knownAce.SecurityIdentifier, out securityIdentifier))
					{
						RawSecurityDescriptor rawSecurityDescriptor2 = new RawSecurityDescriptor("D:");
						rawSecurityDescriptor2.DiscretionaryAcl.InsertAce(0, knownAce);
						MrsTracer.Service.Debug("Mapped ACE {0} to {1}", new object[]
						{
							CommonUtils.GetSDDLString(rawSecurityDescriptor2),
							securityIdentifier
						});
						sourceSD.DiscretionaryAcl.RemoveAce(i);
						knownAce.SecurityIdentifier = securityIdentifier;
						sourceSD.DiscretionaryAcl.InsertAce(i, knownAce);
					}
					else if (flag)
					{
						sourceSD.DiscretionaryAcl.RemoveAce(i);
					}
				}
			}
			return rawSecurityDescriptor;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00019940 File Offset: 0x00017B40
		public void TranslateFolderACL(PropValueData[][] folderACL)
		{
			if (folderACL == null)
			{
				return;
			}
			this.ResolveMappings();
			foreach (PropValueData[] array in folderACL)
			{
				if (array != null)
				{
					foreach (PropValueData pvd in array)
					{
						this.TranslatePropValue(pvd);
					}
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00019994 File Offset: 0x00017B94
		public void TranslateRules(RuleData[] rules)
		{
			if (rules != null && rules.Length > 0)
			{
				this.ResolveMappings();
				foreach (RuleData ruleData in rules)
				{
					ruleData.Enumerate(null, new CommonUtils.EnumPropValueDelegate(this.TranslatePropValue), new CommonUtils.EnumAdrEntryDelegate(this.TranslateAdrEntry));
				}
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000199E4 File Offset: 0x00017BE4
		private static bool IsParticipantEntryIdTag(int propTag)
		{
			return propTag == 268370178;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00019A00 File Offset: 0x00017C00
		private static bool IsSearchKeyTag(int propTag)
		{
			if (propTag <= 5505282)
			{
				if (propTag <= 5308674)
				{
					if (propTag != 3866882 && propTag != 5308674)
					{
						return false;
					}
				}
				else if (propTag != 5374210 && propTag != 5439746 && propTag != 5505282)
				{
					return false;
				}
			}
			else if (propTag <= 6226178)
			{
				if (propTag != 5636354 && propTag != 6029570 && propTag != 6226178)
				{
					return false;
				}
			}
			else if (propTag != 203227394 && propTag != 1080099074 && propTag != 1080295682)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00019A88 File Offset: 0x00017C88
		private static string LegDNFromParticipantEntryId(byte[] entryId)
		{
			if (entryId == null || entryId.Length == 0)
			{
				return null;
			}
			ADParticipantEntryId adparticipantEntryId = ParticipantEntryId.TryFromEntryId(entryId) as ADParticipantEntryId;
			if (adparticipantEntryId != null)
			{
				Participant.Builder builder = new Participant.Builder();
				builder.SetPropertiesFrom(adparticipantEntryId);
				return builder.ToParticipant().EmailAddress;
			}
			return null;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00019AC8 File Offset: 0x00017CC8
		private static byte[] ParticipanEntryIdFromLegDN(byte[] originalEntryId, string newLegDN)
		{
			if (originalEntryId == null || originalEntryId.Length == 0)
			{
				return null;
			}
			ADParticipantEntryId adparticipantEntryId = ParticipantEntryId.TryFromEntryId(originalEntryId) as ADParticipantEntryId;
			if (adparticipantEntryId == null)
			{
				return null;
			}
			Participant.Builder builder = new Participant.Builder();
			builder.SetPropertiesFrom(adparticipantEntryId);
			builder.EmailAddress = newLegDN;
			ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(builder.ToParticipant(), ParticipantEntryIdConsumer.RecipientTableSecondary);
			if (participantEntryId == null)
			{
				return null;
			}
			return participantEntryId.ToByteArray();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00019B1C File Offset: 0x00017D1C
		private static string LegDNFromSearchKey(byte[] searchKey)
		{
			if (searchKey == null || searchKey.Length == 0)
			{
				return null;
			}
			string text;
			try
			{
				text = Encoding.ASCII.GetString(searchKey);
			}
			catch (DecoderFallbackException)
			{
				return null;
			}
			if (text.StartsWith("EX:", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring(3, text.Length - 3);
				string text2 = text;
				char[] trimChars = new char[1];
				return text2.TrimEnd(trimChars);
			}
			return null;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00019B88 File Offset: 0x00017D88
		private static byte[] SearchKeyFromLegDN(string newLegDN)
		{
			if (newLegDN == null)
			{
				return null;
			}
			byte[] result;
			try
			{
				result = Encoding.ASCII.GetBytes("EX:" + newLegDN + "\0");
			}
			catch (EncoderFallbackException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00019BD0 File Offset: 0x00017DD0
		private void ResolveMappings()
		{
			ICollection<Guid> resolvedKeys = this.sourceMapper.ByMailboxGuid.ResolvedKeys;
			this.targetMapper.ByMailboxGuid.AddKeys(resolvedKeys);
			ICollection<string> resolvedKeys2 = this.sourceMapper.ByX500Proxy.ResolvedKeys;
			this.targetMapper.ByX500Proxy.AddKeys(resolvedKeys2);
			this.targetMapper.LookupUnresolvedKeys();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00019C2C File Offset: 0x00017E2C
		private bool TryTranslateSid(SecurityIdentifier sid, out SecurityIdentifier mappedSID)
		{
			if (sid == null)
			{
				mappedSID = null;
				return false;
			}
			mappedSID = null;
			MappedPrincipal mappedPrincipal = this.sourceMapper.BySid[sid];
			if (mappedPrincipal == null)
			{
				MrsTracer.Service.Debug("{0} is not mappable on the source, ignoring", new object[]
				{
					sid
				});
				return false;
			}
			MappedPrincipal mappedPrincipal2 = null;
			if (mappedPrincipal.MailboxGuid != Guid.Empty)
			{
				mappedPrincipal2 = this.targetMapper.ByMailboxGuid[mappedPrincipal.MailboxGuid];
			}
			if (mappedPrincipal2 == null && !string.IsNullOrEmpty(mappedPrincipal.LegacyDN))
			{
				mappedPrincipal2 = this.targetMapper.ByX500Proxy[mappedPrincipal.LegacyDN];
			}
			if (mappedPrincipal2 != null)
			{
				mappedSID = mappedPrincipal2.ObjectSid;
			}
			return mappedSID != null;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00019CE4 File Offset: 0x00017EE4
		private void EnumeratePropValue(PropValueData pvd)
		{
			string text = null;
			if (PrincipalTranslator.IsParticipantEntryIdTag(pvd.PropTag))
			{
				text = PrincipalTranslator.LegDNFromParticipantEntryId(pvd.Value as byte[]);
			}
			else if (PrincipalTranslator.IsSearchKeyTag(pvd.PropTag))
			{
				text = PrincipalTranslator.LegDNFromSearchKey(pvd.Value as byte[]);
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.sourceMapper.AddLegDN(text);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00019D48 File Offset: 0x00017F48
		private void TranslatePropValue(PropValueData pvd)
		{
			if (PrincipalTranslator.IsParticipantEntryIdTag(pvd.PropTag))
			{
				string text = PrincipalTranslator.LegDNFromParticipantEntryId(pvd.Value as byte[]);
				if (text != null)
				{
					string text2 = this.targetMapper.LookupLegDnByExProxy(text);
					pvd.Value = PrincipalTranslator.ParticipanEntryIdFromLegDN(pvd.Value as byte[], text2);
					MrsTracer.Service.Debug("Translating '{0}' to '{1}'", new object[]
					{
						text,
						text2
					});
					return;
				}
			}
			else if (PrincipalTranslator.IsSearchKeyTag(pvd.PropTag))
			{
				string text = PrincipalTranslator.LegDNFromSearchKey(pvd.Value as byte[]);
				if (text != null)
				{
					string text3 = this.targetMapper.LookupLegDnByExProxy(text);
					pvd.Value = PrincipalTranslator.SearchKeyFromLegDN(text3);
					MrsTracer.Service.Debug("Translating '{0}' to '{1}'", new object[]
					{
						text,
						text3
					});
				}
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00019E1C File Offset: 0x0001801C
		private void EnumerateAdrEntry(AdrEntryData aed)
		{
			foreach (PropValueData propValueData in aed.Values)
			{
				string text = null;
				PropTag propTag = (PropTag)propValueData.PropTag;
				if (propTag != PropTag.EntryId)
				{
					if (propTag == PropTag.SearchKey)
					{
						text = PrincipalTranslator.LegDNFromSearchKey(propValueData.Value as byte[]);
					}
				}
				else
				{
					text = PrincipalTranslator.LegDNFromParticipantEntryId(propValueData.Value as byte[]);
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.sourceMapper.AddLegDN(text);
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00019E98 File Offset: 0x00018098
		private void TranslateAdrEntry(AdrEntryData aed)
		{
			foreach (PropValueData propValueData in aed.Values)
			{
				PropTag propTag = (PropTag)propValueData.PropTag;
				if (propTag != PropTag.EntryId)
				{
					if (propTag == PropTag.SearchKey)
					{
						string text = PrincipalTranslator.LegDNFromSearchKey(propValueData.Value as byte[]);
						if (text != null)
						{
							string text2 = this.targetMapper.LookupLegDnByExProxy(text);
							propValueData.Value = PrincipalTranslator.SearchKeyFromLegDN(text2);
							MrsTracer.Service.Debug("Translating '{0}' to '{1}'", new object[]
							{
								text,
								text2
							});
						}
					}
				}
				else
				{
					string text = PrincipalTranslator.LegDNFromParticipantEntryId(propValueData.Value as byte[]);
					if (text != null)
					{
						string text3 = this.targetMapper.LookupLegDnByExProxy(text);
						propValueData.Value = PrincipalTranslator.ParticipanEntryIdFromLegDN(propValueData.Value as byte[], text3);
						MrsTracer.Service.Debug("Translating '{0}' to '{1}'", new object[]
						{
							text,
							text3
						});
					}
				}
			}
		}

		// Token: 0x040001DD RID: 477
		private PrincipalMapper sourceMapper;

		// Token: 0x040001DE RID: 478
		private PrincipalMapper targetMapper;
	}
}
