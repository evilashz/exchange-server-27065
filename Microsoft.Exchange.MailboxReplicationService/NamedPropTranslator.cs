using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004A RID: 74
	internal class NamedPropTranslator
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x00018EFB File Offset: 0x000170FB
		public NamedPropTranslator(Action<List<BadMessageRec>> reportBadItemsDelegate, NamedPropMapper sourceMapper, NamedPropMapper targetMapper)
		{
			this.reportBadItemsDelegate = reportBadItemsDelegate;
			this.sourceMapper = sourceMapper;
			this.targetMapper = targetMapper;
			this.hasUnresolvedMappings = true;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00018FB0 File Offset: 0x000171B0
		public void EnumerateRestriction(FolderRec folderRec, BadItemKind badItemKind, RestrictionData rest)
		{
			if (rest != null)
			{
				CommonUtils.ProcessKnownExceptions(delegate
				{
					rest.EnumeratePropTags(new CommonUtils.EnumPropTagDelegate(this.EnumeratePtag));
				}, delegate(Exception ex)
				{
					if (this.reportBadItemsDelegate != null && CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
					{
						WellKnownException.DataProviderPermanent,
						WellKnownException.CorruptData
					}))
					{
						List<BadMessageRec> list = new List<BadMessageRec>(1);
						list.Add(BadMessageRec.Folder(folderRec, badItemKind, ex));
						this.reportBadItemsDelegate(list);
						return true;
					}
					return false;
				});
				this.hasUnresolvedMappings = true;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00019038 File Offset: 0x00017238
		public void EnumerateSortOrder(SortOrderData sortOrder)
		{
			if (sortOrder != null)
			{
				sortOrder.Enumerate(delegate(SortOrderMember som)
				{
					int propTag = som.PropTag;
					this.EnumeratePtag(ref propTag);
				});
				this.hasUnresolvedMappings = true;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00019068 File Offset: 0x00017268
		public void EnumerateRules(RuleData[] rules)
		{
			if (rules != null)
			{
				foreach (RuleData ruleData in rules)
				{
					ruleData.Enumerate(new CommonUtils.EnumPropTagDelegate(this.EnumeratePtag), null, null);
				}
				this.hasUnresolvedMappings = true;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000190A8 File Offset: 0x000172A8
		public void EnumeratePropTags(PropTag[] ptags)
		{
			if (ptags != null)
			{
				foreach (int num in ptags)
				{
					this.EnumeratePtag(ref num);
				}
				this.hasUnresolvedMappings = true;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000190D9 File Offset: 0x000172D9
		public void TranslateRestriction(RestrictionData rest)
		{
			if (rest != null)
			{
				this.ResolveMappingsIfNeeded();
				rest.EnumeratePropTags(new CommonUtils.EnumPropTagDelegate(this.TranslatePtag));
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001911C File Offset: 0x0001731C
		public void TranslateSortOrder(SortOrderData so)
		{
			if (so != null)
			{
				this.ResolveMappingsIfNeeded();
				so.Enumerate(delegate(SortOrderMember som)
				{
					int propTag = som.PropTag;
					this.TranslatePtag(ref propTag);
					som.PropTag = propTag;
				});
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001914C File Offset: 0x0001734C
		public void TranslateRules(RuleData[] rules)
		{
			if (rules != null)
			{
				this.ResolveMappingsIfNeeded();
				foreach (RuleData ruleData in rules)
				{
					ruleData.Enumerate(new CommonUtils.EnumPropTagDelegate(this.TranslatePtag), null, null);
				}
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001918C File Offset: 0x0001738C
		public void TranslatePropTags(PropTag[] ptags)
		{
			if (ptags != null)
			{
				this.ResolveMappingsIfNeeded();
				for (int i = 0; i < ptags.Length; i++)
				{
					int num = (int)ptags[i];
					this.TranslatePtag(ref num);
					ptags[i] = (PropTag)num;
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000191C0 File Offset: 0x000173C0
		public void Clear()
		{
			this.sourceMapper.Clear();
			this.targetMapper.Clear();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000191D8 File Offset: 0x000173D8
		private void ResolveMappingsIfNeeded()
		{
			if (!this.hasUnresolvedMappings)
			{
				return;
			}
			ICollection<NamedPropData> resolvedKeys = this.sourceMapper.ByNamedProp.ResolvedKeys;
			this.targetMapper.ByNamedProp.AddKeys(resolvedKeys);
			this.targetMapper.LookupUnresolvedKeys();
			this.hasUnresolvedMappings = false;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00019224 File Offset: 0x00017424
		private void EnumeratePtag(ref int ptag)
		{
			PropTag propTag = (PropTag)ptag;
			if (!propTag.IsNamedProperty())
			{
				return;
			}
			this.sourceMapper.ById.AddKey(propTag.Id());
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00019254 File Offset: 0x00017454
		private void TranslatePtag(ref int ptag)
		{
			PropTag propTag = (PropTag)ptag;
			if (!propTag.IsNamedProperty())
			{
				return;
			}
			NamedPropMapper.Mapping mapping = this.sourceMapper.ById[propTag.Id()];
			if (mapping == null)
			{
				MrsTracer.Service.Warning("Proptag 0x{0:X} could not be mapped to a namedprop in the source mailbox, not translating.", new object[]
				{
					propTag
				});
				return;
			}
			NamedPropMapper.Mapping mapping2 = this.targetMapper.ByNamedProp[mapping.NPData];
			if (mapping2 != null)
			{
				ptag = (int)PropTagHelper.PropTagFromIdAndType(mapping2.PropId, propTag.ValueType());
				return;
			}
			MrsTracer.Service.Warning("NamedProp {0} (source ptag 0x{1:X}) could not be mapped in the target mailbox, not translating.", new object[]
			{
				mapping.NPData,
				propTag
			});
		}

		// Token: 0x040001AC RID: 428
		private NamedPropMapper sourceMapper;

		// Token: 0x040001AD RID: 429
		private NamedPropMapper targetMapper;

		// Token: 0x040001AE RID: 430
		private bool hasUnresolvedMappings;

		// Token: 0x040001AF RID: 431
		private Action<List<BadMessageRec>> reportBadItemsDelegate;
	}
}
