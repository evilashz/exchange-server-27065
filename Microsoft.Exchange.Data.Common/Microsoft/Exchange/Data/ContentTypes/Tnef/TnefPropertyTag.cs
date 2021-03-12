using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000ED RID: 237
	public struct TnefPropertyTag
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x000318DF File Offset: 0x0002FADF
		public TnefPropertyTag(TnefPropertyId id, TnefPropertyType type)
		{
			this.tag = ((int)((ushort)id) << 16 | (int)((ushort)type));
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000318EF File Offset: 0x0002FAEF
		public TnefPropertyTag(int tag)
		{
			this.tag = tag;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000318F8 File Offset: 0x0002FAF8
		public TnefPropertyId Id
		{
			get
			{
				return (TnefPropertyId)(this.tag >> 16);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00031904 File Offset: 0x0002FB04
		public TnefPropertyType TnefType
		{
			get
			{
				return (TnefPropertyType)(this.tag & 65535);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00031913 File Offset: 0x0002FB13
		public bool IsMultiValued
		{
			get
			{
				return (this.tag & 4096) != 0;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00031927 File Offset: 0x0002FB27
		public TnefPropertyType ValueTnefType
		{
			get
			{
				return this.TnefType & (TnefPropertyType)(-4097);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00031936 File Offset: 0x0002FB36
		public bool IsNamed
		{
			get
			{
				return ((long)this.tag & (long)((ulong)int.MinValue)) != 0L;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00031950 File Offset: 0x0002FB50
		public bool IsTnefTypeValid
		{
			get
			{
				TnefPropertyType valueTnefType = this.ValueTnefType;
				TnefPropertyType tnefPropertyType = valueTnefType;
				if (tnefPropertyType <= TnefPropertyType.Unicode)
				{
					switch (tnefPropertyType)
					{
					case TnefPropertyType.Unspecified:
					case TnefPropertyType.Null:
					case (TnefPropertyType)8:
					case (TnefPropertyType)9:
					case TnefPropertyType.Error:
					case (TnefPropertyType)12:
					case (TnefPropertyType)14:
					case (TnefPropertyType)15:
					case (TnefPropertyType)16:
					case (TnefPropertyType)17:
					case (TnefPropertyType)18:
					case (TnefPropertyType)19:
						break;
					case TnefPropertyType.I2:
					case TnefPropertyType.Long:
					case TnefPropertyType.R4:
					case TnefPropertyType.Double:
					case TnefPropertyType.Currency:
					case TnefPropertyType.AppTime:
					case TnefPropertyType.I8:
						return true;
					case TnefPropertyType.Boolean:
					case TnefPropertyType.Object:
						return !this.IsMultiValued;
					default:
						switch (tnefPropertyType)
						{
						case TnefPropertyType.String8:
						case TnefPropertyType.Unicode:
							return true;
						}
						break;
					}
				}
				else if (tnefPropertyType == TnefPropertyType.SysTime || tnefPropertyType == TnefPropertyType.ClassId || tnefPropertyType == TnefPropertyType.Binary)
				{
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000319F7 File Offset: 0x0002FBF7
		public static implicit operator int(TnefPropertyTag tag)
		{
			return tag.tag;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00031A00 File Offset: 0x0002FC00
		public static implicit operator TnefPropertyTag(int tag)
		{
			return new TnefPropertyTag(tag);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00031A08 File Offset: 0x0002FC08
		public TnefPropertyTag ToUnicode()
		{
			if (this.ValueTnefType == TnefPropertyType.String8)
			{
				return new TnefPropertyTag(this.Id, this.IsMultiValued ? ((TnefPropertyType)4127) : TnefPropertyType.Unicode);
			}
			return this;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00031A37 File Offset: 0x0002FC37
		public override int GetHashCode()
		{
			return this.tag;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00031A40 File Offset: 0x0002FC40
		public override string ToString()
		{
			string text = this.Id.ToString("G");
			if (char.IsDigit(text[0]))
			{
				text = this.Id.ToString("X");
			}
			string text2 = this.ValueTnefType.ToString("G");
			if (char.IsDigit(text2[0]))
			{
				text2 = this.ValueTnefType.ToString("X");
			}
			if (this.IsMultiValued)
			{
				text2 += "+MV";
			}
			return text + " (" + text2 + ")";
		}

		// Token: 0x04000A19 RID: 2585
		internal const TnefPropertyId ReadReceiptDisplayName = (TnefPropertyId)16427;

		// Token: 0x04000A1A RID: 2586
		internal const TnefPropertyId ReadReceiptEmailAddress = (TnefPropertyId)16426;

		// Token: 0x04000A1B RID: 2587
		internal const TnefPropertyId ReadReceiptAddrtype = (TnefPropertyId)16425;

		// Token: 0x04000A1C RID: 2588
		internal const TnefPropertyId ReadReceiptSmtpAddress = (TnefPropertyId)3725;

		// Token: 0x04000A1D RID: 2589
		public static readonly TnefPropertyTag Null = new TnefPropertyTag(TnefPropertyId.Null, TnefPropertyType.Unspecified);

		// Token: 0x04000A1E RID: 2590
		public static readonly TnefPropertyTag AcknowledgementMode = new TnefPropertyTag(TnefPropertyId.Null, TnefPropertyType.Long);

		// Token: 0x04000A1F RID: 2591
		public static readonly TnefPropertyTag AlternateRecipientAllowed = new TnefPropertyTag(TnefPropertyId.AlternateRecipientAllowed, TnefPropertyType.Boolean);

		// Token: 0x04000A20 RID: 2592
		public static readonly TnefPropertyTag AuthorizingUsers = new TnefPropertyTag(TnefPropertyId.AuthorizingUsers, TnefPropertyType.Binary);

		// Token: 0x04000A21 RID: 2593
		public static readonly TnefPropertyTag AutoForwardCommentA = new TnefPropertyTag(TnefPropertyId.AutoForwardComment, TnefPropertyType.String8);

		// Token: 0x04000A22 RID: 2594
		public static readonly TnefPropertyTag AutoForwardCommentW = new TnefPropertyTag(TnefPropertyId.AutoForwardComment, TnefPropertyType.Unicode);

		// Token: 0x04000A23 RID: 2595
		public static readonly TnefPropertyTag AutoForwarded = new TnefPropertyTag(TnefPropertyId.AutoForwarded, TnefPropertyType.Boolean);

		// Token: 0x04000A24 RID: 2596
		public static readonly TnefPropertyTag ContentConfidentialityAlgorithmId = new TnefPropertyTag(TnefPropertyId.ContentConfidentialityAlgorithmId, TnefPropertyType.Binary);

		// Token: 0x04000A25 RID: 2597
		public static readonly TnefPropertyTag ContentCorrelator = new TnefPropertyTag(TnefPropertyId.ContentCorrelator, TnefPropertyType.Binary);

		// Token: 0x04000A26 RID: 2598
		public static readonly TnefPropertyTag ContentIdentifierA = new TnefPropertyTag(TnefPropertyId.ContentIdentifier, TnefPropertyType.String8);

		// Token: 0x04000A27 RID: 2599
		public static readonly TnefPropertyTag ContentIdentifierW = new TnefPropertyTag(TnefPropertyId.ContentIdentifier, TnefPropertyType.Unicode);

		// Token: 0x04000A28 RID: 2600
		public static readonly TnefPropertyTag ContentLength = new TnefPropertyTag(TnefPropertyId.ContentLength, TnefPropertyType.Long);

		// Token: 0x04000A29 RID: 2601
		public static readonly TnefPropertyTag ContentReturnRequested = new TnefPropertyTag(TnefPropertyId.ContentReturnRequested, TnefPropertyType.Boolean);

		// Token: 0x04000A2A RID: 2602
		public static readonly TnefPropertyTag ConversationKey = new TnefPropertyTag(TnefPropertyId.ConversationKey, TnefPropertyType.Binary);

		// Token: 0x04000A2B RID: 2603
		public static readonly TnefPropertyTag ConversionEits = new TnefPropertyTag(TnefPropertyId.ConversionEits, TnefPropertyType.Binary);

		// Token: 0x04000A2C RID: 2604
		public static readonly TnefPropertyTag ConversionWithLossProhibited = new TnefPropertyTag(TnefPropertyId.ConversionWithLossProhibited, TnefPropertyType.Boolean);

		// Token: 0x04000A2D RID: 2605
		public static readonly TnefPropertyTag ConvertedEits = new TnefPropertyTag(TnefPropertyId.ConvertedEits, TnefPropertyType.Binary);

		// Token: 0x04000A2E RID: 2606
		public static readonly TnefPropertyTag DeferredDeliveryTime = new TnefPropertyTag(TnefPropertyId.DeferredDeliveryTime, TnefPropertyType.SysTime);

		// Token: 0x04000A2F RID: 2607
		public static readonly TnefPropertyTag DeliverTime = new TnefPropertyTag(TnefPropertyId.DeliverTime, TnefPropertyType.SysTime);

		// Token: 0x04000A30 RID: 2608
		public static readonly TnefPropertyTag DiscardReason = new TnefPropertyTag(TnefPropertyId.DiscardReason, TnefPropertyType.Long);

		// Token: 0x04000A31 RID: 2609
		public static readonly TnefPropertyTag DisclosureOfRecipients = new TnefPropertyTag(TnefPropertyId.DisclosureOfRecipients, TnefPropertyType.Boolean);

		// Token: 0x04000A32 RID: 2610
		public static readonly TnefPropertyTag DlExpansionHistory = new TnefPropertyTag(TnefPropertyId.DlExpansionHistory, TnefPropertyType.Binary);

		// Token: 0x04000A33 RID: 2611
		public static readonly TnefPropertyTag DlExpansionProhibited = new TnefPropertyTag(TnefPropertyId.DlExpansionProhibited, TnefPropertyType.Boolean);

		// Token: 0x04000A34 RID: 2612
		public static readonly TnefPropertyTag ExpiryTime = new TnefPropertyTag(TnefPropertyId.ExpiryTime, TnefPropertyType.SysTime);

		// Token: 0x04000A35 RID: 2613
		public static readonly TnefPropertyTag ImplicitConversionProhibited = new TnefPropertyTag(TnefPropertyId.ImplicitConversionProhibited, TnefPropertyType.Boolean);

		// Token: 0x04000A36 RID: 2614
		public static readonly TnefPropertyTag Importance = new TnefPropertyTag(TnefPropertyId.Importance, TnefPropertyType.Long);

		// Token: 0x04000A37 RID: 2615
		public static readonly TnefPropertyTag IpmId = new TnefPropertyTag(TnefPropertyId.IpmId, TnefPropertyType.Binary);

		// Token: 0x04000A38 RID: 2616
		public static readonly TnefPropertyTag LatestDeliveryTime = new TnefPropertyTag(TnefPropertyId.LatestDeliveryTime, TnefPropertyType.SysTime);

		// Token: 0x04000A39 RID: 2617
		public static readonly TnefPropertyTag MessageClassA = new TnefPropertyTag(TnefPropertyId.MessageClass, TnefPropertyType.String8);

		// Token: 0x04000A3A RID: 2618
		public static readonly TnefPropertyTag MessageClassW = new TnefPropertyTag(TnefPropertyId.MessageClass, TnefPropertyType.Unicode);

		// Token: 0x04000A3B RID: 2619
		public static readonly TnefPropertyTag MessageDeliveryId = new TnefPropertyTag(TnefPropertyId.MessageDeliveryId, TnefPropertyType.Binary);

		// Token: 0x04000A3C RID: 2620
		public static readonly TnefPropertyTag MessageSecurityLabel = new TnefPropertyTag(TnefPropertyId.MessageSecurityLabel, TnefPropertyType.Binary);

		// Token: 0x04000A3D RID: 2621
		public static readonly TnefPropertyTag ObsoletedIpms = new TnefPropertyTag(TnefPropertyId.ObsoletedIpms, TnefPropertyType.Binary);

		// Token: 0x04000A3E RID: 2622
		public static readonly TnefPropertyTag OriginallyIntendedRecipientName = new TnefPropertyTag(TnefPropertyId.OriginallyIntendedRecipientName, TnefPropertyType.Binary);

		// Token: 0x04000A3F RID: 2623
		public static readonly TnefPropertyTag OriginalEits = new TnefPropertyTag(TnefPropertyId.OriginalEits, TnefPropertyType.Binary);

		// Token: 0x04000A40 RID: 2624
		public static readonly TnefPropertyTag OriginatorCertificate = new TnefPropertyTag(TnefPropertyId.OriginatorCertificate, TnefPropertyType.Binary);

		// Token: 0x04000A41 RID: 2625
		public static readonly TnefPropertyTag OriginatorDeliveryReportRequested = new TnefPropertyTag(TnefPropertyId.OriginatorDeliveryReportRequested, TnefPropertyType.Boolean);

		// Token: 0x04000A42 RID: 2626
		public static readonly TnefPropertyTag OriginatorReturnAddress = new TnefPropertyTag(TnefPropertyId.OriginatorReturnAddress, TnefPropertyType.Binary);

		// Token: 0x04000A43 RID: 2627
		public static readonly TnefPropertyTag ParentKey = new TnefPropertyTag(TnefPropertyId.ParentKey, TnefPropertyType.Binary);

		// Token: 0x04000A44 RID: 2628
		public static readonly TnefPropertyTag Priority = new TnefPropertyTag(TnefPropertyId.Priority, TnefPropertyType.Long);

		// Token: 0x04000A45 RID: 2629
		public static readonly TnefPropertyTag OriginCheck = new TnefPropertyTag(TnefPropertyId.OriginCheck, TnefPropertyType.Binary);

		// Token: 0x04000A46 RID: 2630
		public static readonly TnefPropertyTag ProofOfSubmissionRequested = new TnefPropertyTag(TnefPropertyId.ProofOfSubmissionRequested, TnefPropertyType.Boolean);

		// Token: 0x04000A47 RID: 2631
		public static readonly TnefPropertyTag ReadReceiptRequested = new TnefPropertyTag(TnefPropertyId.ReadReceiptRequested, TnefPropertyType.Boolean);

		// Token: 0x04000A48 RID: 2632
		public static readonly TnefPropertyTag ReceiptTime = new TnefPropertyTag(TnefPropertyId.ReceiptTime, TnefPropertyType.SysTime);

		// Token: 0x04000A49 RID: 2633
		public static readonly TnefPropertyTag RecipientReassignmentProhibited = new TnefPropertyTag(TnefPropertyId.RecipientReassignmentProhibited, TnefPropertyType.Boolean);

		// Token: 0x04000A4A RID: 2634
		public static readonly TnefPropertyTag RedirectionHistory = new TnefPropertyTag(TnefPropertyId.RedirectionHistory, TnefPropertyType.Binary);

		// Token: 0x04000A4B RID: 2635
		public static readonly TnefPropertyTag RelatedIpms = new TnefPropertyTag(TnefPropertyId.RelatedIpms, TnefPropertyType.Binary);

		// Token: 0x04000A4C RID: 2636
		public static readonly TnefPropertyTag OriginalSensitivity = new TnefPropertyTag(TnefPropertyId.OriginalSensitivity, TnefPropertyType.Long);

		// Token: 0x04000A4D RID: 2637
		public static readonly TnefPropertyTag LanguagesA = new TnefPropertyTag(TnefPropertyId.Languages, TnefPropertyType.String8);

		// Token: 0x04000A4E RID: 2638
		public static readonly TnefPropertyTag LanguagesW = new TnefPropertyTag(TnefPropertyId.Languages, TnefPropertyType.Unicode);

		// Token: 0x04000A4F RID: 2639
		public static readonly TnefPropertyTag ReplyTime = new TnefPropertyTag(TnefPropertyId.ReplyTime, TnefPropertyType.SysTime);

		// Token: 0x04000A50 RID: 2640
		public static readonly TnefPropertyTag ReportTag = new TnefPropertyTag(TnefPropertyId.ReportTag, TnefPropertyType.Binary);

		// Token: 0x04000A51 RID: 2641
		public static readonly TnefPropertyTag ReportTime = new TnefPropertyTag(TnefPropertyId.ReportTime, TnefPropertyType.SysTime);

		// Token: 0x04000A52 RID: 2642
		public static readonly TnefPropertyTag ReturnedIpm = new TnefPropertyTag(TnefPropertyId.ReturnedIpm, TnefPropertyType.Boolean);

		// Token: 0x04000A53 RID: 2643
		public static readonly TnefPropertyTag Security = new TnefPropertyTag(TnefPropertyId.Security, TnefPropertyType.Long);

		// Token: 0x04000A54 RID: 2644
		public static readonly TnefPropertyTag IncompleteCopy = new TnefPropertyTag(TnefPropertyId.IncompleteCopy, TnefPropertyType.Boolean);

		// Token: 0x04000A55 RID: 2645
		public static readonly TnefPropertyTag Sensitivity = new TnefPropertyTag(TnefPropertyId.Sensitivity, TnefPropertyType.Long);

		// Token: 0x04000A56 RID: 2646
		public static readonly TnefPropertyTag SubjectA = new TnefPropertyTag(TnefPropertyId.Subject, TnefPropertyType.String8);

		// Token: 0x04000A57 RID: 2647
		public static readonly TnefPropertyTag SubjectW = new TnefPropertyTag(TnefPropertyId.Subject, TnefPropertyType.Unicode);

		// Token: 0x04000A58 RID: 2648
		public static readonly TnefPropertyTag SubjectIpm = new TnefPropertyTag(TnefPropertyId.SubjectIpm, TnefPropertyType.Binary);

		// Token: 0x04000A59 RID: 2649
		public static readonly TnefPropertyTag ClientSubmitTime = new TnefPropertyTag(TnefPropertyId.ClientSubmitTime, TnefPropertyType.SysTime);

		// Token: 0x04000A5A RID: 2650
		public static readonly TnefPropertyTag ReportNameA = new TnefPropertyTag(TnefPropertyId.ReportName, TnefPropertyType.String8);

		// Token: 0x04000A5B RID: 2651
		public static readonly TnefPropertyTag ReportNameW = new TnefPropertyTag(TnefPropertyId.ReportName, TnefPropertyType.Unicode);

		// Token: 0x04000A5C RID: 2652
		public static readonly TnefPropertyTag SentRepresentingSearchKey = new TnefPropertyTag(TnefPropertyId.SentRepresentingSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A5D RID: 2653
		public static readonly TnefPropertyTag X400ContentType = new TnefPropertyTag(TnefPropertyId.X400ContentType, TnefPropertyType.Binary);

		// Token: 0x04000A5E RID: 2654
		public static readonly TnefPropertyTag SubjectPrefixA = new TnefPropertyTag(TnefPropertyId.SubjectPrefix, TnefPropertyType.String8);

		// Token: 0x04000A5F RID: 2655
		public static readonly TnefPropertyTag SubjectPrefixW = new TnefPropertyTag(TnefPropertyId.SubjectPrefix, TnefPropertyType.Unicode);

		// Token: 0x04000A60 RID: 2656
		public static readonly TnefPropertyTag NonReceiptReason = new TnefPropertyTag(TnefPropertyId.NonReceiptReason, TnefPropertyType.Long);

		// Token: 0x04000A61 RID: 2657
		public static readonly TnefPropertyTag ReceivedByEntryId = new TnefPropertyTag(TnefPropertyId.ReceivedByEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A62 RID: 2658
		public static readonly TnefPropertyTag ReceivedByNameA = new TnefPropertyTag(TnefPropertyId.ReceivedByName, TnefPropertyType.String8);

		// Token: 0x04000A63 RID: 2659
		public static readonly TnefPropertyTag ReceivedByNameW = new TnefPropertyTag(TnefPropertyId.ReceivedByName, TnefPropertyType.Unicode);

		// Token: 0x04000A64 RID: 2660
		public static readonly TnefPropertyTag SentRepresentingEntryId = new TnefPropertyTag(TnefPropertyId.SentRepresentingEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A65 RID: 2661
		public static readonly TnefPropertyTag SentRepresentingNameA = new TnefPropertyTag(TnefPropertyId.SentRepresentingName, TnefPropertyType.String8);

		// Token: 0x04000A66 RID: 2662
		public static readonly TnefPropertyTag SentRepresentingNameW = new TnefPropertyTag(TnefPropertyId.SentRepresentingName, TnefPropertyType.Unicode);

		// Token: 0x04000A67 RID: 2663
		public static readonly TnefPropertyTag RcvdRepresentingEntryId = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A68 RID: 2664
		public static readonly TnefPropertyTag RcvdRepresentingNameA = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingName, TnefPropertyType.String8);

		// Token: 0x04000A69 RID: 2665
		public static readonly TnefPropertyTag RcvdRepresentingNameW = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingName, TnefPropertyType.Unicode);

		// Token: 0x04000A6A RID: 2666
		public static readonly TnefPropertyTag ReportEntryId = new TnefPropertyTag(TnefPropertyId.ReportEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A6B RID: 2667
		public static readonly TnefPropertyTag ReadReceiptEntryId = new TnefPropertyTag(TnefPropertyId.ReadReceiptEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A6C RID: 2668
		public static readonly TnefPropertyTag MessageSubmissionId = new TnefPropertyTag(TnefPropertyId.MessageSubmissionId, TnefPropertyType.Binary);

		// Token: 0x04000A6D RID: 2669
		public static readonly TnefPropertyTag ProviderSubmitTime = new TnefPropertyTag(TnefPropertyId.ProviderSubmitTime, TnefPropertyType.SysTime);

		// Token: 0x04000A6E RID: 2670
		public static readonly TnefPropertyTag OriginalSubjectA = new TnefPropertyTag(TnefPropertyId.OriginalSubject, TnefPropertyType.String8);

		// Token: 0x04000A6F RID: 2671
		public static readonly TnefPropertyTag OriginalSubjectW = new TnefPropertyTag(TnefPropertyId.OriginalSubject, TnefPropertyType.Unicode);

		// Token: 0x04000A70 RID: 2672
		public static readonly TnefPropertyTag DiscVal = new TnefPropertyTag(TnefPropertyId.DiscVal, TnefPropertyType.Boolean);

		// Token: 0x04000A71 RID: 2673
		public static readonly TnefPropertyTag OrigMessageClassA = new TnefPropertyTag(TnefPropertyId.OrigMessageClass, TnefPropertyType.String8);

		// Token: 0x04000A72 RID: 2674
		public static readonly TnefPropertyTag OrigMessageClassW = new TnefPropertyTag(TnefPropertyId.OrigMessageClass, TnefPropertyType.Unicode);

		// Token: 0x04000A73 RID: 2675
		public static readonly TnefPropertyTag OriginalAuthorEntryId = new TnefPropertyTag(TnefPropertyId.OriginalAuthorEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A74 RID: 2676
		public static readonly TnefPropertyTag OriginalAuthorNameA = new TnefPropertyTag(TnefPropertyId.OriginalAuthorName, TnefPropertyType.String8);

		// Token: 0x04000A75 RID: 2677
		public static readonly TnefPropertyTag OriginalAuthorNameW = new TnefPropertyTag(TnefPropertyId.OriginalAuthorName, TnefPropertyType.Unicode);

		// Token: 0x04000A76 RID: 2678
		public static readonly TnefPropertyTag OriginalSubmitTime = new TnefPropertyTag(TnefPropertyId.OriginalSubmitTime, TnefPropertyType.SysTime);

		// Token: 0x04000A77 RID: 2679
		public static readonly TnefPropertyTag ReplyRecipientEntries = new TnefPropertyTag(TnefPropertyId.ReplyRecipientEntries, TnefPropertyType.Binary);

		// Token: 0x04000A78 RID: 2680
		public static readonly TnefPropertyTag ReplyRecipientNamesA = new TnefPropertyTag(TnefPropertyId.ReplyRecipientNames, TnefPropertyType.String8);

		// Token: 0x04000A79 RID: 2681
		public static readonly TnefPropertyTag ReplyRecipientNamesW = new TnefPropertyTag(TnefPropertyId.ReplyRecipientNames, TnefPropertyType.Unicode);

		// Token: 0x04000A7A RID: 2682
		public static readonly TnefPropertyTag ReceivedBySearchKey = new TnefPropertyTag(TnefPropertyId.ReceivedBySearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A7B RID: 2683
		public static readonly TnefPropertyTag RcvdRepresentingSearchKey = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A7C RID: 2684
		public static readonly TnefPropertyTag ReadReceiptSearchKey = new TnefPropertyTag(TnefPropertyId.ReadReceiptSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A7D RID: 2685
		public static readonly TnefPropertyTag ReportSearchKey = new TnefPropertyTag(TnefPropertyId.ReportSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A7E RID: 2686
		public static readonly TnefPropertyTag OriginalDeliveryTime = new TnefPropertyTag(TnefPropertyId.OriginalDeliveryTime, TnefPropertyType.SysTime);

		// Token: 0x04000A7F RID: 2687
		public static readonly TnefPropertyTag OriginalAuthorSearchKey = new TnefPropertyTag(TnefPropertyId.OriginalAuthorSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A80 RID: 2688
		public static readonly TnefPropertyTag MessageToMe = new TnefPropertyTag(TnefPropertyId.MessageToMe, TnefPropertyType.Boolean);

		// Token: 0x04000A81 RID: 2689
		public static readonly TnefPropertyTag MessageCcMe = new TnefPropertyTag(TnefPropertyId.MessageCcMe, TnefPropertyType.Boolean);

		// Token: 0x04000A82 RID: 2690
		public static readonly TnefPropertyTag MessageRecipMe = new TnefPropertyTag(TnefPropertyId.MessageRecipMe, TnefPropertyType.Boolean);

		// Token: 0x04000A83 RID: 2691
		public static readonly TnefPropertyTag OriginalSenderNameA = new TnefPropertyTag(TnefPropertyId.OriginalSenderName, TnefPropertyType.String8);

		// Token: 0x04000A84 RID: 2692
		public static readonly TnefPropertyTag OriginalSenderNameW = new TnefPropertyTag(TnefPropertyId.OriginalSenderName, TnefPropertyType.Unicode);

		// Token: 0x04000A85 RID: 2693
		public static readonly TnefPropertyTag OriginalSenderEntryId = new TnefPropertyTag(TnefPropertyId.OriginalSenderEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A86 RID: 2694
		public static readonly TnefPropertyTag OriginalSenderSearchKey = new TnefPropertyTag(TnefPropertyId.OriginalSenderSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A87 RID: 2695
		public static readonly TnefPropertyTag OriginalSentRepresentingNameA = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingName, TnefPropertyType.String8);

		// Token: 0x04000A88 RID: 2696
		public static readonly TnefPropertyTag OriginalSentRepresentingNameW = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingName, TnefPropertyType.Unicode);

		// Token: 0x04000A89 RID: 2697
		public static readonly TnefPropertyTag OriginalSentRepresentingEntryId = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingEntryId, TnefPropertyType.Binary);

		// Token: 0x04000A8A RID: 2698
		public static readonly TnefPropertyTag OriginalSentRepresentingSearchKey = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000A8B RID: 2699
		public static readonly TnefPropertyTag StartDate = new TnefPropertyTag(TnefPropertyId.StartDate, TnefPropertyType.SysTime);

		// Token: 0x04000A8C RID: 2700
		public static readonly TnefPropertyTag EndDate = new TnefPropertyTag(TnefPropertyId.EndDate, TnefPropertyType.SysTime);

		// Token: 0x04000A8D RID: 2701
		public static readonly TnefPropertyTag OwnerApptId = new TnefPropertyTag(TnefPropertyId.OwnerApptId, TnefPropertyType.Long);

		// Token: 0x04000A8E RID: 2702
		public static readonly TnefPropertyTag ResponseRequested = new TnefPropertyTag(TnefPropertyId.ResponseRequested, TnefPropertyType.Boolean);

		// Token: 0x04000A8F RID: 2703
		public static readonly TnefPropertyTag SentRepresentingAddrtypeA = new TnefPropertyTag(TnefPropertyId.SentRepresentingAddrtype, TnefPropertyType.String8);

		// Token: 0x04000A90 RID: 2704
		public static readonly TnefPropertyTag SentRepresentingAddrtypeW = new TnefPropertyTag(TnefPropertyId.SentRepresentingAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000A91 RID: 2705
		public static readonly TnefPropertyTag SentRepresentingEmailAddressA = new TnefPropertyTag(TnefPropertyId.SentRepresentingEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000A92 RID: 2706
		public static readonly TnefPropertyTag SentRepresentingEmailAddressW = new TnefPropertyTag(TnefPropertyId.SentRepresentingEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000A93 RID: 2707
		public static readonly TnefPropertyTag OriginalSenderAddrtypeA = new TnefPropertyTag(TnefPropertyId.OriginalSenderAddrtype, TnefPropertyType.String8);

		// Token: 0x04000A94 RID: 2708
		public static readonly TnefPropertyTag OriginalSenderAddrtypeW = new TnefPropertyTag(TnefPropertyId.OriginalSenderAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000A95 RID: 2709
		public static readonly TnefPropertyTag OriginalSenderEmailAddressA = new TnefPropertyTag(TnefPropertyId.OriginalSenderEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000A96 RID: 2710
		public static readonly TnefPropertyTag OriginalSenderEmailAddressW = new TnefPropertyTag(TnefPropertyId.OriginalSenderEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000A97 RID: 2711
		public static readonly TnefPropertyTag OriginalSentRepresentingAddrtypeA = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingAddrtype, TnefPropertyType.String8);

		// Token: 0x04000A98 RID: 2712
		public static readonly TnefPropertyTag OriginalSentRepresentingAddrtypeW = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000A99 RID: 2713
		public static readonly TnefPropertyTag OriginalSentRepresentingEmailAddressA = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000A9A RID: 2714
		public static readonly TnefPropertyTag OriginalSentRepresentingEmailAddressW = new TnefPropertyTag(TnefPropertyId.OriginalSentRepresentingEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000A9B RID: 2715
		public static readonly TnefPropertyTag ConversationTopicA = new TnefPropertyTag(TnefPropertyId.ConversationTopic, TnefPropertyType.String8);

		// Token: 0x04000A9C RID: 2716
		public static readonly TnefPropertyTag ConversationTopicW = new TnefPropertyTag(TnefPropertyId.ConversationTopic, TnefPropertyType.Unicode);

		// Token: 0x04000A9D RID: 2717
		public static readonly TnefPropertyTag ConversationIndex = new TnefPropertyTag(TnefPropertyId.ConversationIndex, TnefPropertyType.Binary);

		// Token: 0x04000A9E RID: 2718
		public static readonly TnefPropertyTag OriginalDisplayBccA = new TnefPropertyTag(TnefPropertyId.OriginalDisplayBcc, TnefPropertyType.String8);

		// Token: 0x04000A9F RID: 2719
		public static readonly TnefPropertyTag OriginalDisplayBccW = new TnefPropertyTag(TnefPropertyId.OriginalDisplayBcc, TnefPropertyType.Unicode);

		// Token: 0x04000AA0 RID: 2720
		public static readonly TnefPropertyTag OriginalDisplayCcA = new TnefPropertyTag(TnefPropertyId.OriginalDisplayCc, TnefPropertyType.String8);

		// Token: 0x04000AA1 RID: 2721
		public static readonly TnefPropertyTag OriginalDisplayCcW = new TnefPropertyTag(TnefPropertyId.OriginalDisplayCc, TnefPropertyType.Unicode);

		// Token: 0x04000AA2 RID: 2722
		public static readonly TnefPropertyTag OriginalDisplayToA = new TnefPropertyTag(TnefPropertyId.OriginalDisplayTo, TnefPropertyType.String8);

		// Token: 0x04000AA3 RID: 2723
		public static readonly TnefPropertyTag OriginalDisplayToW = new TnefPropertyTag(TnefPropertyId.OriginalDisplayTo, TnefPropertyType.Unicode);

		// Token: 0x04000AA4 RID: 2724
		public static readonly TnefPropertyTag ReceivedByAddrtypeA = new TnefPropertyTag(TnefPropertyId.ReceivedByAddrtype, TnefPropertyType.String8);

		// Token: 0x04000AA5 RID: 2725
		public static readonly TnefPropertyTag ReceivedByAddrtypeW = new TnefPropertyTag(TnefPropertyId.ReceivedByAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000AA6 RID: 2726
		public static readonly TnefPropertyTag ReceivedByEmailAddressA = new TnefPropertyTag(TnefPropertyId.ReceivedByEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000AA7 RID: 2727
		public static readonly TnefPropertyTag ReceivedByEmailAddressW = new TnefPropertyTag(TnefPropertyId.ReceivedByEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000AA8 RID: 2728
		public static readonly TnefPropertyTag RcvdRepresentingAddrtypeA = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingAddrtype, TnefPropertyType.String8);

		// Token: 0x04000AA9 RID: 2729
		public static readonly TnefPropertyTag RcvdRepresentingAddrtypeW = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000AAA RID: 2730
		public static readonly TnefPropertyTag RcvdRepresentingEmailAddressA = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000AAB RID: 2731
		public static readonly TnefPropertyTag RcvdRepresentingEmailAddressW = new TnefPropertyTag(TnefPropertyId.RcvdRepresentingEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000AAC RID: 2732
		public static readonly TnefPropertyTag OriginalAuthorAddrtypeA = new TnefPropertyTag(TnefPropertyId.OriginalAuthorAddrtype, TnefPropertyType.String8);

		// Token: 0x04000AAD RID: 2733
		public static readonly TnefPropertyTag OriginalAuthorAddrtypeW = new TnefPropertyTag(TnefPropertyId.OriginalAuthorAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000AAE RID: 2734
		public static readonly TnefPropertyTag OriginalAuthorEmailAddressA = new TnefPropertyTag(TnefPropertyId.OriginalAuthorEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000AAF RID: 2735
		public static readonly TnefPropertyTag OriginalAuthorEmailAddressW = new TnefPropertyTag(TnefPropertyId.OriginalAuthorEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000AB0 RID: 2736
		public static readonly TnefPropertyTag OriginallyIntendedRecipAddrtypeA = new TnefPropertyTag(TnefPropertyId.OriginallyIntendedRecipAddrtype, TnefPropertyType.String8);

		// Token: 0x04000AB1 RID: 2737
		public static readonly TnefPropertyTag OriginallyIntendedRecipAddrtypeW = new TnefPropertyTag(TnefPropertyId.OriginallyIntendedRecipAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000AB2 RID: 2738
		public static readonly TnefPropertyTag OriginallyIntendedRecipEmailAddressA = new TnefPropertyTag(TnefPropertyId.OriginallyIntendedRecipEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000AB3 RID: 2739
		public static readonly TnefPropertyTag OriginallyIntendedRecipEmailAddressW = new TnefPropertyTag(TnefPropertyId.OriginallyIntendedRecipEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000AB4 RID: 2740
		public static readonly TnefPropertyTag TransportMessageHeadersA = new TnefPropertyTag(TnefPropertyId.TransportMessageHeaders, TnefPropertyType.String8);

		// Token: 0x04000AB5 RID: 2741
		public static readonly TnefPropertyTag TransportMessageHeadersW = new TnefPropertyTag(TnefPropertyId.TransportMessageHeaders, TnefPropertyType.Unicode);

		// Token: 0x04000AB6 RID: 2742
		public static readonly TnefPropertyTag Delegation = new TnefPropertyTag(TnefPropertyId.Delegation, TnefPropertyType.Binary);

		// Token: 0x04000AB7 RID: 2743
		public static readonly TnefPropertyTag TnefCorrelationKey = new TnefPropertyTag(TnefPropertyId.TnefCorrelationKey, TnefPropertyType.Binary);

		// Token: 0x04000AB8 RID: 2744
		public static readonly TnefPropertyTag ContentIntegrityCheck = new TnefPropertyTag(TnefPropertyId.ContentIntegrityCheck, TnefPropertyType.Binary);

		// Token: 0x04000AB9 RID: 2745
		public static readonly TnefPropertyTag ExplicitConversion = new TnefPropertyTag(TnefPropertyId.ExplicitConversion, TnefPropertyType.Long);

		// Token: 0x04000ABA RID: 2746
		public static readonly TnefPropertyTag IpmReturnRequested = new TnefPropertyTag(TnefPropertyId.IpmReturnRequested, TnefPropertyType.Boolean);

		// Token: 0x04000ABB RID: 2747
		public static readonly TnefPropertyTag MessageToken = new TnefPropertyTag(TnefPropertyId.MessageToken, TnefPropertyType.Binary);

		// Token: 0x04000ABC RID: 2748
		public static readonly TnefPropertyTag NdrReasonCode = new TnefPropertyTag(TnefPropertyId.NdrReasonCode, TnefPropertyType.Long);

		// Token: 0x04000ABD RID: 2749
		public static readonly TnefPropertyTag NdrDiagCode = new TnefPropertyTag(TnefPropertyId.NdrDiagCode, TnefPropertyType.Long);

		// Token: 0x04000ABE RID: 2750
		public static readonly TnefPropertyTag NonReceiptNotificationRequested = new TnefPropertyTag(TnefPropertyId.NonReceiptNotificationRequested, TnefPropertyType.Boolean);

		// Token: 0x04000ABF RID: 2751
		public static readonly TnefPropertyTag DeliveryPoint = new TnefPropertyTag(TnefPropertyId.DeliveryPoint, TnefPropertyType.Long);

		// Token: 0x04000AC0 RID: 2752
		public static readonly TnefPropertyTag OriginatorNonDeliveryReportRequested = new TnefPropertyTag(TnefPropertyId.OriginatorNonDeliveryReportRequested, TnefPropertyType.Boolean);

		// Token: 0x04000AC1 RID: 2753
		public static readonly TnefPropertyTag OriginatorRequestedAlternateRecipient = new TnefPropertyTag(TnefPropertyId.OriginatorRequestedAlternateRecipient, TnefPropertyType.Binary);

		// Token: 0x04000AC2 RID: 2754
		public static readonly TnefPropertyTag PhysicalDeliveryBureauFaxDelivery = new TnefPropertyTag(TnefPropertyId.PhysicalDeliveryBureauFaxDelivery, TnefPropertyType.Boolean);

		// Token: 0x04000AC3 RID: 2755
		public static readonly TnefPropertyTag PhysicalDeliveryMode = new TnefPropertyTag(TnefPropertyId.PhysicalDeliveryMode, TnefPropertyType.Long);

		// Token: 0x04000AC4 RID: 2756
		public static readonly TnefPropertyTag PhysicalDeliveryReportRequest = new TnefPropertyTag(TnefPropertyId.PhysicalDeliveryReportRequest, TnefPropertyType.Long);

		// Token: 0x04000AC5 RID: 2757
		public static readonly TnefPropertyTag PhysicalForwardingAddress = new TnefPropertyTag(TnefPropertyId.PhysicalForwardingAddress, TnefPropertyType.Binary);

		// Token: 0x04000AC6 RID: 2758
		public static readonly TnefPropertyTag PhysicalForwardingAddressRequested = new TnefPropertyTag(TnefPropertyId.PhysicalForwardingAddressRequested, TnefPropertyType.Boolean);

		// Token: 0x04000AC7 RID: 2759
		public static readonly TnefPropertyTag PhysicalForwardingProhibited = new TnefPropertyTag(TnefPropertyId.PhysicalForwardingProhibited, TnefPropertyType.Boolean);

		// Token: 0x04000AC8 RID: 2760
		public static readonly TnefPropertyTag PhysicalRenditionAttributes = new TnefPropertyTag(TnefPropertyId.PhysicalRenditionAttributes, TnefPropertyType.Binary);

		// Token: 0x04000AC9 RID: 2761
		public static readonly TnefPropertyTag ProofOfDelivery = new TnefPropertyTag(TnefPropertyId.ProofOfDelivery, TnefPropertyType.Binary);

		// Token: 0x04000ACA RID: 2762
		public static readonly TnefPropertyTag ProofOfDeliveryRequested = new TnefPropertyTag(TnefPropertyId.ProofOfDeliveryRequested, TnefPropertyType.Boolean);

		// Token: 0x04000ACB RID: 2763
		public static readonly TnefPropertyTag RecipientCertificate = new TnefPropertyTag(TnefPropertyId.RecipientCertificate, TnefPropertyType.Binary);

		// Token: 0x04000ACC RID: 2764
		public static readonly TnefPropertyTag RecipientNumberForAdviceA = new TnefPropertyTag(TnefPropertyId.RecipientNumberForAdvice, TnefPropertyType.String8);

		// Token: 0x04000ACD RID: 2765
		public static readonly TnefPropertyTag RecipientNumberForAdviceW = new TnefPropertyTag(TnefPropertyId.RecipientNumberForAdvice, TnefPropertyType.Unicode);

		// Token: 0x04000ACE RID: 2766
		public static readonly TnefPropertyTag RecipientType = new TnefPropertyTag(TnefPropertyId.RecipientType, TnefPropertyType.Long);

		// Token: 0x04000ACF RID: 2767
		public static readonly TnefPropertyTag RegisteredMailType = new TnefPropertyTag(TnefPropertyId.RegisteredMailType, TnefPropertyType.Long);

		// Token: 0x04000AD0 RID: 2768
		public static readonly TnefPropertyTag ReplyRequested = new TnefPropertyTag(TnefPropertyId.ReplyRequested, TnefPropertyType.Boolean);

		// Token: 0x04000AD1 RID: 2769
		public static readonly TnefPropertyTag RequestedDeliveryMethod = new TnefPropertyTag(TnefPropertyId.RequestedDeliveryMethod, TnefPropertyType.Long);

		// Token: 0x04000AD2 RID: 2770
		public static readonly TnefPropertyTag SenderEntryId = new TnefPropertyTag(TnefPropertyId.SenderEntryId, TnefPropertyType.Binary);

		// Token: 0x04000AD3 RID: 2771
		public static readonly TnefPropertyTag SenderNameA = new TnefPropertyTag(TnefPropertyId.SenderName, TnefPropertyType.String8);

		// Token: 0x04000AD4 RID: 2772
		public static readonly TnefPropertyTag SenderNameW = new TnefPropertyTag(TnefPropertyId.SenderName, TnefPropertyType.Unicode);

		// Token: 0x04000AD5 RID: 2773
		public static readonly TnefPropertyTag SupplementaryInfoA = new TnefPropertyTag(TnefPropertyId.SupplementaryInfo, TnefPropertyType.String8);

		// Token: 0x04000AD6 RID: 2774
		public static readonly TnefPropertyTag SupplementaryInfoW = new TnefPropertyTag(TnefPropertyId.SupplementaryInfo, TnefPropertyType.Unicode);

		// Token: 0x04000AD7 RID: 2775
		public static readonly TnefPropertyTag TypeOfMtsUser = new TnefPropertyTag(TnefPropertyId.TypeOfMtsUser, TnefPropertyType.Long);

		// Token: 0x04000AD8 RID: 2776
		public static readonly TnefPropertyTag SenderSearchKey = new TnefPropertyTag(TnefPropertyId.SenderSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000AD9 RID: 2777
		public static readonly TnefPropertyTag SenderAddrtypeA = new TnefPropertyTag(TnefPropertyId.SenderAddrtype, TnefPropertyType.String8);

		// Token: 0x04000ADA RID: 2778
		public static readonly TnefPropertyTag SenderAddrtypeW = new TnefPropertyTag(TnefPropertyId.SenderAddrtype, TnefPropertyType.Unicode);

		// Token: 0x04000ADB RID: 2779
		public static readonly TnefPropertyTag SenderEmailAddressA = new TnefPropertyTag(TnefPropertyId.SenderEmailAddress, TnefPropertyType.String8);

		// Token: 0x04000ADC RID: 2780
		public static readonly TnefPropertyTag SenderEmailAddressW = new TnefPropertyTag(TnefPropertyId.SenderEmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000ADD RID: 2781
		public static readonly TnefPropertyTag NdrStatusCode = new TnefPropertyTag(TnefPropertyId.NdrStatusCode, TnefPropertyType.Long);

		// Token: 0x04000ADE RID: 2782
		public static readonly TnefPropertyTag CurrentVersion = new TnefPropertyTag(TnefPropertyId.CurrentVersion, TnefPropertyType.I8);

		// Token: 0x04000ADF RID: 2783
		public static readonly TnefPropertyTag DeleteAfterSubmit = new TnefPropertyTag(TnefPropertyId.DeleteAfterSubmit, TnefPropertyType.Boolean);

		// Token: 0x04000AE0 RID: 2784
		public static readonly TnefPropertyTag DisplayBccA = new TnefPropertyTag(TnefPropertyId.DisplayBcc, TnefPropertyType.String8);

		// Token: 0x04000AE1 RID: 2785
		public static readonly TnefPropertyTag DisplayBccW = new TnefPropertyTag(TnefPropertyId.DisplayBcc, TnefPropertyType.Unicode);

		// Token: 0x04000AE2 RID: 2786
		public static readonly TnefPropertyTag DisplayCcA = new TnefPropertyTag(TnefPropertyId.DisplayCc, TnefPropertyType.String8);

		// Token: 0x04000AE3 RID: 2787
		public static readonly TnefPropertyTag DisplayCcW = new TnefPropertyTag(TnefPropertyId.DisplayCc, TnefPropertyType.Unicode);

		// Token: 0x04000AE4 RID: 2788
		public static readonly TnefPropertyTag DisplayToA = new TnefPropertyTag(TnefPropertyId.DisplayTo, TnefPropertyType.String8);

		// Token: 0x04000AE5 RID: 2789
		public static readonly TnefPropertyTag DisplayToW = new TnefPropertyTag(TnefPropertyId.DisplayTo, TnefPropertyType.Unicode);

		// Token: 0x04000AE6 RID: 2790
		public static readonly TnefPropertyTag ParentDisplayA = new TnefPropertyTag(TnefPropertyId.ParentDisplay, TnefPropertyType.String8);

		// Token: 0x04000AE7 RID: 2791
		public static readonly TnefPropertyTag ParentDisplayW = new TnefPropertyTag(TnefPropertyId.ParentDisplay, TnefPropertyType.Unicode);

		// Token: 0x04000AE8 RID: 2792
		public static readonly TnefPropertyTag MessageDeliveryTime = new TnefPropertyTag(TnefPropertyId.MessageDeliveryTime, TnefPropertyType.SysTime);

		// Token: 0x04000AE9 RID: 2793
		public static readonly TnefPropertyTag MessageFlags = new TnefPropertyTag(TnefPropertyId.MessageFlags, TnefPropertyType.Long);

		// Token: 0x04000AEA RID: 2794
		public static readonly TnefPropertyTag MessageSize = new TnefPropertyTag(TnefPropertyId.MessageSize, TnefPropertyType.Long);

		// Token: 0x04000AEB RID: 2795
		public static readonly TnefPropertyTag ParentEntryId = new TnefPropertyTag(TnefPropertyId.ParentEntryId, TnefPropertyType.Binary);

		// Token: 0x04000AEC RID: 2796
		public static readonly TnefPropertyTag SentmailEntryId = new TnefPropertyTag(TnefPropertyId.SentmailEntryId, TnefPropertyType.Binary);

		// Token: 0x04000AED RID: 2797
		public static readonly TnefPropertyTag Correlate = new TnefPropertyTag(TnefPropertyId.Correlate, TnefPropertyType.Boolean);

		// Token: 0x04000AEE RID: 2798
		public static readonly TnefPropertyTag CorrelateMtsid = new TnefPropertyTag(TnefPropertyId.CorrelateMtsid, TnefPropertyType.Binary);

		// Token: 0x04000AEF RID: 2799
		public static readonly TnefPropertyTag DiscreteValues = new TnefPropertyTag(TnefPropertyId.DiscreteValues, TnefPropertyType.Boolean);

		// Token: 0x04000AF0 RID: 2800
		public static readonly TnefPropertyTag Responsibility = new TnefPropertyTag(TnefPropertyId.Responsibility, TnefPropertyType.Boolean);

		// Token: 0x04000AF1 RID: 2801
		public static readonly TnefPropertyTag SpoolerStatus = new TnefPropertyTag(TnefPropertyId.SpoolerStatus, TnefPropertyType.Long);

		// Token: 0x04000AF2 RID: 2802
		public static readonly TnefPropertyTag TransportStatus = new TnefPropertyTag(TnefPropertyId.TransportStatus, TnefPropertyType.Long);

		// Token: 0x04000AF3 RID: 2803
		public static readonly TnefPropertyTag MessageRecipients = new TnefPropertyTag(TnefPropertyId.MessageRecipients, TnefPropertyType.Object);

		// Token: 0x04000AF4 RID: 2804
		public static readonly TnefPropertyTag MessageAttachments = new TnefPropertyTag(TnefPropertyId.MessageAttachments, TnefPropertyType.Object);

		// Token: 0x04000AF5 RID: 2805
		public static readonly TnefPropertyTag SubmitFlags = new TnefPropertyTag(TnefPropertyId.SubmitFlags, TnefPropertyType.Long);

		// Token: 0x04000AF6 RID: 2806
		public static readonly TnefPropertyTag RecipientStatus = new TnefPropertyTag(TnefPropertyId.RecipientStatus, TnefPropertyType.Long);

		// Token: 0x04000AF7 RID: 2807
		public static readonly TnefPropertyTag TransportKey = new TnefPropertyTag(TnefPropertyId.TransportKey, TnefPropertyType.Long);

		// Token: 0x04000AF8 RID: 2808
		public static readonly TnefPropertyTag MsgStatus = new TnefPropertyTag(TnefPropertyId.MsgStatus, TnefPropertyType.Long);

		// Token: 0x04000AF9 RID: 2809
		public static readonly TnefPropertyTag MessageDownloadTime = new TnefPropertyTag(TnefPropertyId.MessageDownloadTime, TnefPropertyType.Long);

		// Token: 0x04000AFA RID: 2810
		public static readonly TnefPropertyTag CreationVersion = new TnefPropertyTag(TnefPropertyId.CreationVersion, TnefPropertyType.I8);

		// Token: 0x04000AFB RID: 2811
		public static readonly TnefPropertyTag ModifyVersion = new TnefPropertyTag(TnefPropertyId.ModifyVersion, TnefPropertyType.I8);

		// Token: 0x04000AFC RID: 2812
		public static readonly TnefPropertyTag Hasattach = new TnefPropertyTag(TnefPropertyId.Hasattach, TnefPropertyType.Boolean);

		// Token: 0x04000AFD RID: 2813
		public static readonly TnefPropertyTag BodyCrc = new TnefPropertyTag(TnefPropertyId.BodyCrc, TnefPropertyType.Long);

		// Token: 0x04000AFE RID: 2814
		public static readonly TnefPropertyTag NormalizedSubjectA = new TnefPropertyTag(TnefPropertyId.NormalizedSubject, TnefPropertyType.String8);

		// Token: 0x04000AFF RID: 2815
		public static readonly TnefPropertyTag NormalizedSubjectW = new TnefPropertyTag(TnefPropertyId.NormalizedSubject, TnefPropertyType.Unicode);

		// Token: 0x04000B00 RID: 2816
		public static readonly TnefPropertyTag RtfInSync = new TnefPropertyTag(TnefPropertyId.RtfInSync, TnefPropertyType.Boolean);

		// Token: 0x04000B01 RID: 2817
		public static readonly TnefPropertyTag AttachSize = new TnefPropertyTag(TnefPropertyId.AttachSize, TnefPropertyType.Long);

		// Token: 0x04000B02 RID: 2818
		public static readonly TnefPropertyTag AttachNum = new TnefPropertyTag(TnefPropertyId.AttachNum, TnefPropertyType.Long);

		// Token: 0x04000B03 RID: 2819
		public static readonly TnefPropertyTag Preprocess = new TnefPropertyTag(TnefPropertyId.Preprocess, TnefPropertyType.Boolean);

		// Token: 0x04000B04 RID: 2820
		public static readonly TnefPropertyTag InternetArticleNumber = new TnefPropertyTag(TnefPropertyId.InternetArticleNumber, TnefPropertyType.Long);

		// Token: 0x04000B05 RID: 2821
		public static readonly TnefPropertyTag NewsgroupNameA = new TnefPropertyTag(TnefPropertyId.NewsgroupName, TnefPropertyType.String8);

		// Token: 0x04000B06 RID: 2822
		public static readonly TnefPropertyTag NewsgroupNameW = new TnefPropertyTag(TnefPropertyId.NewsgroupName, TnefPropertyType.Unicode);

		// Token: 0x04000B07 RID: 2823
		public static readonly TnefPropertyTag OriginatingMtaCertificate = new TnefPropertyTag(TnefPropertyId.OriginatingMtaCertificate, TnefPropertyType.Binary);

		// Token: 0x04000B08 RID: 2824
		public static readonly TnefPropertyTag ProofOfSubmission = new TnefPropertyTag(TnefPropertyId.ProofOfSubmission, TnefPropertyType.Binary);

		// Token: 0x04000B09 RID: 2825
		public static readonly TnefPropertyTag NtSecurityDescriptor = new TnefPropertyTag(TnefPropertyId.NtSecurityDescriptor, TnefPropertyType.Binary);

		// Token: 0x04000B0A RID: 2826
		public static readonly TnefPropertyTag Access = new TnefPropertyTag(TnefPropertyId.Access, TnefPropertyType.Long);

		// Token: 0x04000B0B RID: 2827
		public static readonly TnefPropertyTag RowType = new TnefPropertyTag(TnefPropertyId.RowType, TnefPropertyType.Long);

		// Token: 0x04000B0C RID: 2828
		public static readonly TnefPropertyTag InstanceKey = new TnefPropertyTag(TnefPropertyId.InstanceKey, TnefPropertyType.Binary);

		// Token: 0x04000B0D RID: 2829
		public static readonly TnefPropertyTag AccessLevel = new TnefPropertyTag(TnefPropertyId.AccessLevel, TnefPropertyType.Long);

		// Token: 0x04000B0E RID: 2830
		public static readonly TnefPropertyTag MappingSignature = new TnefPropertyTag(TnefPropertyId.MappingSignature, TnefPropertyType.Binary);

		// Token: 0x04000B0F RID: 2831
		public static readonly TnefPropertyTag RecordKey = new TnefPropertyTag(TnefPropertyId.RecordKey, TnefPropertyType.Binary);

		// Token: 0x04000B10 RID: 2832
		public static readonly TnefPropertyTag StoreRecordKey = new TnefPropertyTag(TnefPropertyId.StoreRecordKey, TnefPropertyType.Binary);

		// Token: 0x04000B11 RID: 2833
		public static readonly TnefPropertyTag StoreEntryId = new TnefPropertyTag(TnefPropertyId.StoreEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B12 RID: 2834
		public static readonly TnefPropertyTag MiniIcon = new TnefPropertyTag(TnefPropertyId.MiniIcon, TnefPropertyType.Binary);

		// Token: 0x04000B13 RID: 2835
		public static readonly TnefPropertyTag Icon = new TnefPropertyTag(TnefPropertyId.Icon, TnefPropertyType.Binary);

		// Token: 0x04000B14 RID: 2836
		public static readonly TnefPropertyTag ObjectType = new TnefPropertyTag(TnefPropertyId.ObjectType, TnefPropertyType.Long);

		// Token: 0x04000B15 RID: 2837
		public static readonly TnefPropertyTag EntryId = new TnefPropertyTag(TnefPropertyId.EntryId, TnefPropertyType.Binary);

		// Token: 0x04000B16 RID: 2838
		public static readonly TnefPropertyTag BodyA = new TnefPropertyTag(TnefPropertyId.Body, TnefPropertyType.String8);

		// Token: 0x04000B17 RID: 2839
		public static readonly TnefPropertyTag BodyW = new TnefPropertyTag(TnefPropertyId.Body, TnefPropertyType.Unicode);

		// Token: 0x04000B18 RID: 2840
		public static readonly TnefPropertyTag ReportTextA = new TnefPropertyTag(TnefPropertyId.ReportText, TnefPropertyType.String8);

		// Token: 0x04000B19 RID: 2841
		public static readonly TnefPropertyTag ReportTextW = new TnefPropertyTag(TnefPropertyId.ReportText, TnefPropertyType.Unicode);

		// Token: 0x04000B1A RID: 2842
		public static readonly TnefPropertyTag OriginatorAndDlExpansionHistory = new TnefPropertyTag(TnefPropertyId.OriginatorAndDlExpansionHistory, TnefPropertyType.Binary);

		// Token: 0x04000B1B RID: 2843
		public static readonly TnefPropertyTag ReportingDlName = new TnefPropertyTag(TnefPropertyId.ReportingDlName, TnefPropertyType.Binary);

		// Token: 0x04000B1C RID: 2844
		public static readonly TnefPropertyTag ReportingMtaCertificate = new TnefPropertyTag(TnefPropertyId.ReportingMtaCertificate, TnefPropertyType.Binary);

		// Token: 0x04000B1D RID: 2845
		public static readonly TnefPropertyTag RtfSyncBodyCrc = new TnefPropertyTag(TnefPropertyId.RtfSyncBodyCrc, TnefPropertyType.Long);

		// Token: 0x04000B1E RID: 2846
		public static readonly TnefPropertyTag RtfSyncBodyCount = new TnefPropertyTag(TnefPropertyId.RtfSyncBodyCount, TnefPropertyType.Long);

		// Token: 0x04000B1F RID: 2847
		public static readonly TnefPropertyTag RtfSyncBodyTagA = new TnefPropertyTag(TnefPropertyId.RtfSyncBodyTag, TnefPropertyType.String8);

		// Token: 0x04000B20 RID: 2848
		public static readonly TnefPropertyTag RtfSyncBodyTagW = new TnefPropertyTag(TnefPropertyId.RtfSyncBodyTag, TnefPropertyType.Unicode);

		// Token: 0x04000B21 RID: 2849
		public static readonly TnefPropertyTag RtfCompressed = new TnefPropertyTag(TnefPropertyId.RtfCompressed, TnefPropertyType.Binary);

		// Token: 0x04000B22 RID: 2850
		public static readonly TnefPropertyTag RtfSyncPrefixCount = new TnefPropertyTag(TnefPropertyId.RtfSyncPrefixCount, TnefPropertyType.Long);

		// Token: 0x04000B23 RID: 2851
		public static readonly TnefPropertyTag RtfSyncTrailingCount = new TnefPropertyTag(TnefPropertyId.RtfSyncTrailingCount, TnefPropertyType.Long);

		// Token: 0x04000B24 RID: 2852
		public static readonly TnefPropertyTag OriginallyIntendedRecipEntryId = new TnefPropertyTag(TnefPropertyId.OriginallyIntendedRecipEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B25 RID: 2853
		public static readonly TnefPropertyTag BodyHtmlB = new TnefPropertyTag(TnefPropertyId.BodyHtml, TnefPropertyType.Binary);

		// Token: 0x04000B26 RID: 2854
		public static readonly TnefPropertyTag BodyHtmlA = new TnefPropertyTag(TnefPropertyId.BodyHtml, TnefPropertyType.String8);

		// Token: 0x04000B27 RID: 2855
		public static readonly TnefPropertyTag BodyHtmlW = new TnefPropertyTag(TnefPropertyId.BodyHtml, TnefPropertyType.Unicode);

		// Token: 0x04000B28 RID: 2856
		public static readonly TnefPropertyTag BodyContentLocationA = new TnefPropertyTag(TnefPropertyId.BodyContentLocation, TnefPropertyType.String8);

		// Token: 0x04000B29 RID: 2857
		public static readonly TnefPropertyTag BodyContentLocationW = new TnefPropertyTag(TnefPropertyId.BodyContentLocation, TnefPropertyType.Unicode);

		// Token: 0x04000B2A RID: 2858
		public static readonly TnefPropertyTag BodyContentIdA = new TnefPropertyTag(TnefPropertyId.BodyContentId, TnefPropertyType.String8);

		// Token: 0x04000B2B RID: 2859
		public static readonly TnefPropertyTag BodyContentIdW = new TnefPropertyTag(TnefPropertyId.BodyContentId, TnefPropertyType.Unicode);

		// Token: 0x04000B2C RID: 2860
		public static readonly TnefPropertyTag InternetApprovedA = new TnefPropertyTag(TnefPropertyId.InternetApproved, TnefPropertyType.String8);

		// Token: 0x04000B2D RID: 2861
		public static readonly TnefPropertyTag InternetApprovedW = new TnefPropertyTag(TnefPropertyId.InternetApproved, TnefPropertyType.Unicode);

		// Token: 0x04000B2E RID: 2862
		public static readonly TnefPropertyTag InternetControlA = new TnefPropertyTag(TnefPropertyId.InternetControl, TnefPropertyType.String8);

		// Token: 0x04000B2F RID: 2863
		public static readonly TnefPropertyTag InternetControlW = new TnefPropertyTag(TnefPropertyId.InternetControl, TnefPropertyType.Unicode);

		// Token: 0x04000B30 RID: 2864
		public static readonly TnefPropertyTag InternetDistributionA = new TnefPropertyTag(TnefPropertyId.InternetDistribution, TnefPropertyType.String8);

		// Token: 0x04000B31 RID: 2865
		public static readonly TnefPropertyTag InternetDistributionW = new TnefPropertyTag(TnefPropertyId.InternetDistribution, TnefPropertyType.Unicode);

		// Token: 0x04000B32 RID: 2866
		public static readonly TnefPropertyTag InternetFollowupToA = new TnefPropertyTag(TnefPropertyId.InternetFollowupTo, TnefPropertyType.String8);

		// Token: 0x04000B33 RID: 2867
		public static readonly TnefPropertyTag InternetFollowupToW = new TnefPropertyTag(TnefPropertyId.InternetFollowupTo, TnefPropertyType.Unicode);

		// Token: 0x04000B34 RID: 2868
		public static readonly TnefPropertyTag InternetLines = new TnefPropertyTag(TnefPropertyId.InternetLines, TnefPropertyType.Long);

		// Token: 0x04000B35 RID: 2869
		public static readonly TnefPropertyTag InternetMessageIdA = new TnefPropertyTag(TnefPropertyId.InternetMessageId, TnefPropertyType.String8);

		// Token: 0x04000B36 RID: 2870
		public static readonly TnefPropertyTag InternetMessageIdW = new TnefPropertyTag(TnefPropertyId.InternetMessageId, TnefPropertyType.Unicode);

		// Token: 0x04000B37 RID: 2871
		public static readonly TnefPropertyTag InternetNewsgroupsA = new TnefPropertyTag(TnefPropertyId.InternetNewsgroups, TnefPropertyType.String8);

		// Token: 0x04000B38 RID: 2872
		public static readonly TnefPropertyTag InternetNewsgroupsW = new TnefPropertyTag(TnefPropertyId.InternetNewsgroups, TnefPropertyType.Unicode);

		// Token: 0x04000B39 RID: 2873
		public static readonly TnefPropertyTag InternetOrganizationA = new TnefPropertyTag(TnefPropertyId.InternetOrganization, TnefPropertyType.String8);

		// Token: 0x04000B3A RID: 2874
		public static readonly TnefPropertyTag InternetOrganizationW = new TnefPropertyTag(TnefPropertyId.InternetOrganization, TnefPropertyType.Unicode);

		// Token: 0x04000B3B RID: 2875
		public static readonly TnefPropertyTag InternetNntpPathA = new TnefPropertyTag(TnefPropertyId.InternetNntpPath, TnefPropertyType.String8);

		// Token: 0x04000B3C RID: 2876
		public static readonly TnefPropertyTag InternetNntpPathW = new TnefPropertyTag(TnefPropertyId.InternetNntpPath, TnefPropertyType.Unicode);

		// Token: 0x04000B3D RID: 2877
		public static readonly TnefPropertyTag InternetReferencesA = new TnefPropertyTag(TnefPropertyId.InternetReferences, TnefPropertyType.String8);

		// Token: 0x04000B3E RID: 2878
		public static readonly TnefPropertyTag InternetReferencesW = new TnefPropertyTag(TnefPropertyId.InternetReferences, TnefPropertyType.Unicode);

		// Token: 0x04000B3F RID: 2879
		public static readonly TnefPropertyTag SupersedesA = new TnefPropertyTag(TnefPropertyId.Supersedes, TnefPropertyType.String8);

		// Token: 0x04000B40 RID: 2880
		public static readonly TnefPropertyTag SupersedesW = new TnefPropertyTag(TnefPropertyId.Supersedes, TnefPropertyType.Unicode);

		// Token: 0x04000B41 RID: 2881
		public static readonly TnefPropertyTag PostFolderEntries = new TnefPropertyTag(TnefPropertyId.PostFolderEntries, TnefPropertyType.Binary);

		// Token: 0x04000B42 RID: 2882
		public static readonly TnefPropertyTag PostFolderNamesA = new TnefPropertyTag(TnefPropertyId.PostFolderNames, TnefPropertyType.String8);

		// Token: 0x04000B43 RID: 2883
		public static readonly TnefPropertyTag PostFolderNamesW = new TnefPropertyTag(TnefPropertyId.PostFolderNames, TnefPropertyType.Unicode);

		// Token: 0x04000B44 RID: 2884
		public static readonly TnefPropertyTag PostReplyFolderEntries = new TnefPropertyTag(TnefPropertyId.PostReplyFolderEntries, TnefPropertyType.Binary);

		// Token: 0x04000B45 RID: 2885
		public static readonly TnefPropertyTag PostReplyFolderNamesA = new TnefPropertyTag(TnefPropertyId.PostReplyFolderNames, TnefPropertyType.String8);

		// Token: 0x04000B46 RID: 2886
		public static readonly TnefPropertyTag PostReplyFolderNamesW = new TnefPropertyTag(TnefPropertyId.PostReplyFolderNames, TnefPropertyType.Unicode);

		// Token: 0x04000B47 RID: 2887
		public static readonly TnefPropertyTag PostReplyDenied = new TnefPropertyTag(TnefPropertyId.PostReplyDenied, TnefPropertyType.Boolean);

		// Token: 0x04000B48 RID: 2888
		public static readonly TnefPropertyTag NntpXrefA = new TnefPropertyTag(TnefPropertyId.NntpXref, TnefPropertyType.String8);

		// Token: 0x04000B49 RID: 2889
		public static readonly TnefPropertyTag NntpXrefW = new TnefPropertyTag(TnefPropertyId.NntpXref, TnefPropertyType.Unicode);

		// Token: 0x04000B4A RID: 2890
		public static readonly TnefPropertyTag InternetPrecedenceA = new TnefPropertyTag(TnefPropertyId.InternetPrecedence, TnefPropertyType.String8);

		// Token: 0x04000B4B RID: 2891
		public static readonly TnefPropertyTag InternetPrecedenceW = new TnefPropertyTag(TnefPropertyId.InternetPrecedence, TnefPropertyType.Unicode);

		// Token: 0x04000B4C RID: 2892
		public static readonly TnefPropertyTag InReplyToIdA = new TnefPropertyTag(TnefPropertyId.InReplyToId, TnefPropertyType.String8);

		// Token: 0x04000B4D RID: 2893
		public static readonly TnefPropertyTag InReplyToIdW = new TnefPropertyTag(TnefPropertyId.InReplyToId, TnefPropertyType.Unicode);

		// Token: 0x04000B4E RID: 2894
		public static readonly TnefPropertyTag ListHelpA = new TnefPropertyTag(TnefPropertyId.ListHelp, TnefPropertyType.String8);

		// Token: 0x04000B4F RID: 2895
		public static readonly TnefPropertyTag ListHelpW = new TnefPropertyTag(TnefPropertyId.ListHelp, TnefPropertyType.Unicode);

		// Token: 0x04000B50 RID: 2896
		public static readonly TnefPropertyTag ListSubscribeA = new TnefPropertyTag(TnefPropertyId.ListSubscribe, TnefPropertyType.String8);

		// Token: 0x04000B51 RID: 2897
		public static readonly TnefPropertyTag ListSubscribeW = new TnefPropertyTag(TnefPropertyId.ListSubscribe, TnefPropertyType.Unicode);

		// Token: 0x04000B52 RID: 2898
		public static readonly TnefPropertyTag ListUnsubscribeA = new TnefPropertyTag(TnefPropertyId.ListUnsubscribe, TnefPropertyType.String8);

		// Token: 0x04000B53 RID: 2899
		public static readonly TnefPropertyTag ListUnsubscribeW = new TnefPropertyTag(TnefPropertyId.ListUnsubscribe, TnefPropertyType.Unicode);

		// Token: 0x04000B54 RID: 2900
		public static readonly TnefPropertyTag Rowid = new TnefPropertyTag(TnefPropertyId.Rowid, TnefPropertyType.Long);

		// Token: 0x04000B55 RID: 2901
		public static readonly TnefPropertyTag DisplayNameA = new TnefPropertyTag(TnefPropertyId.DisplayName, TnefPropertyType.String8);

		// Token: 0x04000B56 RID: 2902
		public static readonly TnefPropertyTag DisplayNameW = new TnefPropertyTag(TnefPropertyId.DisplayName, TnefPropertyType.Unicode);

		// Token: 0x04000B57 RID: 2903
		public static readonly TnefPropertyTag AddrtypeA = new TnefPropertyTag(TnefPropertyId.Addrtype, TnefPropertyType.String8);

		// Token: 0x04000B58 RID: 2904
		public static readonly TnefPropertyTag AddrtypeW = new TnefPropertyTag(TnefPropertyId.Addrtype, TnefPropertyType.Unicode);

		// Token: 0x04000B59 RID: 2905
		public static readonly TnefPropertyTag EmailAddressA = new TnefPropertyTag(TnefPropertyId.EmailAddress, TnefPropertyType.String8);

		// Token: 0x04000B5A RID: 2906
		public static readonly TnefPropertyTag EmailAddressW = new TnefPropertyTag(TnefPropertyId.EmailAddress, TnefPropertyType.Unicode);

		// Token: 0x04000B5B RID: 2907
		public static readonly TnefPropertyTag CommentA = new TnefPropertyTag(TnefPropertyId.Comment, TnefPropertyType.String8);

		// Token: 0x04000B5C RID: 2908
		public static readonly TnefPropertyTag CommentW = new TnefPropertyTag(TnefPropertyId.Comment, TnefPropertyType.Unicode);

		// Token: 0x04000B5D RID: 2909
		public static readonly TnefPropertyTag Depth = new TnefPropertyTag(TnefPropertyId.Depth, TnefPropertyType.Long);

		// Token: 0x04000B5E RID: 2910
		public static readonly TnefPropertyTag ProviderDisplayA = new TnefPropertyTag(TnefPropertyId.ProviderDisplay, TnefPropertyType.String8);

		// Token: 0x04000B5F RID: 2911
		public static readonly TnefPropertyTag ProviderDisplayW = new TnefPropertyTag(TnefPropertyId.ProviderDisplay, TnefPropertyType.Unicode);

		// Token: 0x04000B60 RID: 2912
		public static readonly TnefPropertyTag CreationTime = new TnefPropertyTag(TnefPropertyId.CreationTime, TnefPropertyType.SysTime);

		// Token: 0x04000B61 RID: 2913
		public static readonly TnefPropertyTag LastModificationTime = new TnefPropertyTag(TnefPropertyId.LastModificationTime, TnefPropertyType.SysTime);

		// Token: 0x04000B62 RID: 2914
		public static readonly TnefPropertyTag ResourceFlags = new TnefPropertyTag(TnefPropertyId.ResourceFlags, TnefPropertyType.Long);

		// Token: 0x04000B63 RID: 2915
		public static readonly TnefPropertyTag ProviderDllNameA = new TnefPropertyTag(TnefPropertyId.ProviderDllName, TnefPropertyType.String8);

		// Token: 0x04000B64 RID: 2916
		public static readonly TnefPropertyTag ProviderDllNameW = new TnefPropertyTag(TnefPropertyId.ProviderDllName, TnefPropertyType.Unicode);

		// Token: 0x04000B65 RID: 2917
		public static readonly TnefPropertyTag SearchKey = new TnefPropertyTag(TnefPropertyId.SearchKey, TnefPropertyType.Binary);

		// Token: 0x04000B66 RID: 2918
		public static readonly TnefPropertyTag ProviderUid = new TnefPropertyTag(TnefPropertyId.ProviderUid, TnefPropertyType.Binary);

		// Token: 0x04000B67 RID: 2919
		public static readonly TnefPropertyTag ProviderOrdinal = new TnefPropertyTag(TnefPropertyId.ProviderOrdinal, TnefPropertyType.Long);

		// Token: 0x04000B68 RID: 2920
		public static readonly TnefPropertyTag PuidA = new TnefPropertyTag(TnefPropertyId.Puid, TnefPropertyType.String8);

		// Token: 0x04000B69 RID: 2921
		public static readonly TnefPropertyTag PuidW = new TnefPropertyTag(TnefPropertyId.Puid, TnefPropertyType.Unicode);

		// Token: 0x04000B6A RID: 2922
		public static readonly TnefPropertyTag OrigEntryId = new TnefPropertyTag(TnefPropertyId.OrigEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B6B RID: 2923
		public static readonly TnefPropertyTag FormVersionA = new TnefPropertyTag(TnefPropertyId.FormVersion, TnefPropertyType.String8);

		// Token: 0x04000B6C RID: 2924
		public static readonly TnefPropertyTag FormVersionW = new TnefPropertyTag(TnefPropertyId.FormVersion, TnefPropertyType.Unicode);

		// Token: 0x04000B6D RID: 2925
		public static readonly TnefPropertyTag FormClsid = new TnefPropertyTag(TnefPropertyId.FormClsid, TnefPropertyType.ClassId);

		// Token: 0x04000B6E RID: 2926
		public static readonly TnefPropertyTag FormContactNameA = new TnefPropertyTag(TnefPropertyId.FormContactName, TnefPropertyType.String8);

		// Token: 0x04000B6F RID: 2927
		public static readonly TnefPropertyTag FormContactNameW = new TnefPropertyTag(TnefPropertyId.FormContactName, TnefPropertyType.Unicode);

		// Token: 0x04000B70 RID: 2928
		public static readonly TnefPropertyTag FormCategoryA = new TnefPropertyTag(TnefPropertyId.FormCategory, TnefPropertyType.String8);

		// Token: 0x04000B71 RID: 2929
		public static readonly TnefPropertyTag FormCategoryW = new TnefPropertyTag(TnefPropertyId.FormCategory, TnefPropertyType.Unicode);

		// Token: 0x04000B72 RID: 2930
		public static readonly TnefPropertyTag FormCategorySubA = new TnefPropertyTag(TnefPropertyId.FormCategorySub, TnefPropertyType.String8);

		// Token: 0x04000B73 RID: 2931
		public static readonly TnefPropertyTag FormCategorySubW = new TnefPropertyTag(TnefPropertyId.FormCategorySub, TnefPropertyType.Unicode);

		// Token: 0x04000B74 RID: 2932
		public static readonly TnefPropertyTag FormHostMap = new TnefPropertyTag(TnefPropertyId.FormHostMap, (TnefPropertyType)4099);

		// Token: 0x04000B75 RID: 2933
		public static readonly TnefPropertyTag FormHidden = new TnefPropertyTag(TnefPropertyId.FormHidden, TnefPropertyType.Boolean);

		// Token: 0x04000B76 RID: 2934
		public static readonly TnefPropertyTag FormDesignerNameA = new TnefPropertyTag(TnefPropertyId.FormDesignerName, TnefPropertyType.String8);

		// Token: 0x04000B77 RID: 2935
		public static readonly TnefPropertyTag FormDesignerNameW = new TnefPropertyTag(TnefPropertyId.FormDesignerName, TnefPropertyType.Unicode);

		// Token: 0x04000B78 RID: 2936
		public static readonly TnefPropertyTag FormDesignerGuid = new TnefPropertyTag(TnefPropertyId.FormDesignerGuid, TnefPropertyType.ClassId);

		// Token: 0x04000B79 RID: 2937
		public static readonly TnefPropertyTag FormMessageBehavior = new TnefPropertyTag(TnefPropertyId.FormMessageBehavior, TnefPropertyType.Long);

		// Token: 0x04000B7A RID: 2938
		public static readonly TnefPropertyTag DefaultStore = new TnefPropertyTag(TnefPropertyId.DefaultStore, TnefPropertyType.Boolean);

		// Token: 0x04000B7B RID: 2939
		public static readonly TnefPropertyTag StoreSupportMask = new TnefPropertyTag(TnefPropertyId.StoreSupportMask, TnefPropertyType.Long);

		// Token: 0x04000B7C RID: 2940
		public static readonly TnefPropertyTag StoreState = new TnefPropertyTag(TnefPropertyId.StoreState, TnefPropertyType.Long);

		// Token: 0x04000B7D RID: 2941
		public static readonly TnefPropertyTag IpmSubtreeSearchKey = new TnefPropertyTag(TnefPropertyId.IpmSubtreeSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000B7E RID: 2942
		public static readonly TnefPropertyTag IpmOutboxSearchKey = new TnefPropertyTag(TnefPropertyId.IpmOutboxSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000B7F RID: 2943
		public static readonly TnefPropertyTag IpmWastebasketSearchKey = new TnefPropertyTag(TnefPropertyId.IpmWastebasketSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000B80 RID: 2944
		public static readonly TnefPropertyTag IpmSentmailSearchKey = new TnefPropertyTag(TnefPropertyId.IpmSentmailSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000B81 RID: 2945
		public static readonly TnefPropertyTag MdbProvider = new TnefPropertyTag(TnefPropertyId.MdbProvider, TnefPropertyType.Binary);

		// Token: 0x04000B82 RID: 2946
		public static readonly TnefPropertyTag ReceiveFolderSettings = new TnefPropertyTag(TnefPropertyId.ReceiveFolderSettings, TnefPropertyType.Object);

		// Token: 0x04000B83 RID: 2947
		public static readonly TnefPropertyTag ValidFolderMask = new TnefPropertyTag(TnefPropertyId.ValidFolderMask, TnefPropertyType.Long);

		// Token: 0x04000B84 RID: 2948
		public static readonly TnefPropertyTag IpmSubtreeEntryId = new TnefPropertyTag(TnefPropertyId.IpmSubtreeEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B85 RID: 2949
		public static readonly TnefPropertyTag IpmOutboxEntryId = new TnefPropertyTag(TnefPropertyId.IpmOutboxEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B86 RID: 2950
		public static readonly TnefPropertyTag IpmWastebasketEntryId = new TnefPropertyTag(TnefPropertyId.IpmWastebasketEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B87 RID: 2951
		public static readonly TnefPropertyTag IpmSentmailEntryId = new TnefPropertyTag(TnefPropertyId.IpmSentmailEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B88 RID: 2952
		public static readonly TnefPropertyTag ViewsEntryId = new TnefPropertyTag(TnefPropertyId.ViewsEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B89 RID: 2953
		public static readonly TnefPropertyTag CommonViewsEntryId = new TnefPropertyTag(TnefPropertyId.CommonViewsEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B8A RID: 2954
		public static readonly TnefPropertyTag FinderEntryId = new TnefPropertyTag(TnefPropertyId.FinderEntryId, TnefPropertyType.Binary);

		// Token: 0x04000B8B RID: 2955
		public static readonly TnefPropertyTag ContainerFlags = new TnefPropertyTag(TnefPropertyId.ContainerFlags, TnefPropertyType.Long);

		// Token: 0x04000B8C RID: 2956
		public static readonly TnefPropertyTag FolderType = new TnefPropertyTag(TnefPropertyId.FolderType, TnefPropertyType.Long);

		// Token: 0x04000B8D RID: 2957
		public static readonly TnefPropertyTag ContentCount = new TnefPropertyTag(TnefPropertyId.ContentCount, TnefPropertyType.Long);

		// Token: 0x04000B8E RID: 2958
		public static readonly TnefPropertyTag ContentUnread = new TnefPropertyTag(TnefPropertyId.ContentUnread, TnefPropertyType.Long);

		// Token: 0x04000B8F RID: 2959
		public static readonly TnefPropertyTag CreateTemplates = new TnefPropertyTag(TnefPropertyId.CreateTemplates, TnefPropertyType.Object);

		// Token: 0x04000B90 RID: 2960
		public static readonly TnefPropertyTag DetailsTable = new TnefPropertyTag(TnefPropertyId.DetailsTable, TnefPropertyType.Object);

		// Token: 0x04000B91 RID: 2961
		public static readonly TnefPropertyTag Search = new TnefPropertyTag(TnefPropertyId.Search, TnefPropertyType.Object);

		// Token: 0x04000B92 RID: 2962
		public static readonly TnefPropertyTag Selectable = new TnefPropertyTag(TnefPropertyId.Selectable, TnefPropertyType.Boolean);

		// Token: 0x04000B93 RID: 2963
		public static readonly TnefPropertyTag Subfolders = new TnefPropertyTag(TnefPropertyId.Subfolders, TnefPropertyType.Boolean);

		// Token: 0x04000B94 RID: 2964
		public static readonly TnefPropertyTag Status = new TnefPropertyTag(TnefPropertyId.Status, TnefPropertyType.Long);

		// Token: 0x04000B95 RID: 2965
		public static readonly TnefPropertyTag AnrA = new TnefPropertyTag(TnefPropertyId.Anr, TnefPropertyType.String8);

		// Token: 0x04000B96 RID: 2966
		public static readonly TnefPropertyTag AnrW = new TnefPropertyTag(TnefPropertyId.Anr, TnefPropertyType.Unicode);

		// Token: 0x04000B97 RID: 2967
		public static readonly TnefPropertyTag ContentsSortOrder = new TnefPropertyTag(TnefPropertyId.ContentsSortOrder, (TnefPropertyType)4099);

		// Token: 0x04000B98 RID: 2968
		public static readonly TnefPropertyTag ContainerHierarchy = new TnefPropertyTag(TnefPropertyId.ContainerHierarchy, TnefPropertyType.Object);

		// Token: 0x04000B99 RID: 2969
		public static readonly TnefPropertyTag ContainerContents = new TnefPropertyTag(TnefPropertyId.ContainerContents, TnefPropertyType.Object);

		// Token: 0x04000B9A RID: 2970
		public static readonly TnefPropertyTag FolderAssociatedContents = new TnefPropertyTag(TnefPropertyId.FolderAssociatedContents, TnefPropertyType.Object);

		// Token: 0x04000B9B RID: 2971
		public static readonly TnefPropertyTag DefCreateDl = new TnefPropertyTag(TnefPropertyId.DefCreateDl, TnefPropertyType.Binary);

		// Token: 0x04000B9C RID: 2972
		public static readonly TnefPropertyTag DefCreateMailuser = new TnefPropertyTag(TnefPropertyId.DefCreateMailuser, TnefPropertyType.Binary);

		// Token: 0x04000B9D RID: 2973
		public static readonly TnefPropertyTag ContainerClassA = new TnefPropertyTag(TnefPropertyId.ContainerClass, TnefPropertyType.String8);

		// Token: 0x04000B9E RID: 2974
		public static readonly TnefPropertyTag ContainerClassW = new TnefPropertyTag(TnefPropertyId.ContainerClass, TnefPropertyType.Unicode);

		// Token: 0x04000B9F RID: 2975
		public static readonly TnefPropertyTag ContainerModifyVersion = new TnefPropertyTag(TnefPropertyId.ContainerModifyVersion, TnefPropertyType.I8);

		// Token: 0x04000BA0 RID: 2976
		public static readonly TnefPropertyTag AbProviderId = new TnefPropertyTag(TnefPropertyId.AbProviderId, TnefPropertyType.Binary);

		// Token: 0x04000BA1 RID: 2977
		public static readonly TnefPropertyTag DefaultViewEntryId = new TnefPropertyTag(TnefPropertyId.DefaultViewEntryId, TnefPropertyType.Binary);

		// Token: 0x04000BA2 RID: 2978
		public static readonly TnefPropertyTag AssocContentCount = new TnefPropertyTag(TnefPropertyId.AssocContentCount, TnefPropertyType.Long);

		// Token: 0x04000BA3 RID: 2979
		public static readonly TnefPropertyTag ExpandBeginTime = new TnefPropertyTag(TnefPropertyId.ExpandBeginTime, TnefPropertyType.SysTime);

		// Token: 0x04000BA4 RID: 2980
		public static readonly TnefPropertyTag ExpandEndTime = new TnefPropertyTag(TnefPropertyId.ExpandEndTime, TnefPropertyType.SysTime);

		// Token: 0x04000BA5 RID: 2981
		public static readonly TnefPropertyTag ExpandedBeginTime = new TnefPropertyTag(TnefPropertyId.ExpandedBeginTime, TnefPropertyType.SysTime);

		// Token: 0x04000BA6 RID: 2982
		public static readonly TnefPropertyTag ExpandedEndTime = new TnefPropertyTag(TnefPropertyId.ExpandedEndTime, TnefPropertyType.SysTime);

		// Token: 0x04000BA7 RID: 2983
		public static readonly TnefPropertyTag AttachmentX400Parameters = new TnefPropertyTag(TnefPropertyId.AttachmentX400Parameters, TnefPropertyType.Binary);

		// Token: 0x04000BA8 RID: 2984
		public static readonly TnefPropertyTag AttachDataObj = new TnefPropertyTag(TnefPropertyId.AttachData, TnefPropertyType.Object);

		// Token: 0x04000BA9 RID: 2985
		public static readonly TnefPropertyTag AttachDataBin = new TnefPropertyTag(TnefPropertyId.AttachData, TnefPropertyType.Binary);

		// Token: 0x04000BAA RID: 2986
		public static readonly TnefPropertyTag AttachEncoding = new TnefPropertyTag(TnefPropertyId.AttachEncoding, TnefPropertyType.Binary);

		// Token: 0x04000BAB RID: 2987
		public static readonly TnefPropertyTag AttachExtensionA = new TnefPropertyTag(TnefPropertyId.AttachExtension, TnefPropertyType.String8);

		// Token: 0x04000BAC RID: 2988
		public static readonly TnefPropertyTag AttachExtensionW = new TnefPropertyTag(TnefPropertyId.AttachExtension, TnefPropertyType.Unicode);

		// Token: 0x04000BAD RID: 2989
		public static readonly TnefPropertyTag AttachFilenameA = new TnefPropertyTag(TnefPropertyId.AttachFilename, TnefPropertyType.String8);

		// Token: 0x04000BAE RID: 2990
		public static readonly TnefPropertyTag AttachFilenameW = new TnefPropertyTag(TnefPropertyId.AttachFilename, TnefPropertyType.Unicode);

		// Token: 0x04000BAF RID: 2991
		public static readonly TnefPropertyTag AttachMethod = new TnefPropertyTag(TnefPropertyId.AttachMethod, TnefPropertyType.Long);

		// Token: 0x04000BB0 RID: 2992
		public static readonly TnefPropertyTag AttachLongFilenameA = new TnefPropertyTag(TnefPropertyId.AttachLongFilename, TnefPropertyType.String8);

		// Token: 0x04000BB1 RID: 2993
		public static readonly TnefPropertyTag AttachLongFilenameW = new TnefPropertyTag(TnefPropertyId.AttachLongFilename, TnefPropertyType.Unicode);

		// Token: 0x04000BB2 RID: 2994
		public static readonly TnefPropertyTag AttachPathnameA = new TnefPropertyTag(TnefPropertyId.AttachPathname, TnefPropertyType.String8);

		// Token: 0x04000BB3 RID: 2995
		public static readonly TnefPropertyTag AttachPathnameW = new TnefPropertyTag(TnefPropertyId.AttachPathname, TnefPropertyType.Unicode);

		// Token: 0x04000BB4 RID: 2996
		public static readonly TnefPropertyTag AttachRendering = new TnefPropertyTag(TnefPropertyId.AttachRendering, TnefPropertyType.Binary);

		// Token: 0x04000BB5 RID: 2997
		public static readonly TnefPropertyTag AttachTag = new TnefPropertyTag(TnefPropertyId.AttachTag, TnefPropertyType.Binary);

		// Token: 0x04000BB6 RID: 2998
		public static readonly TnefPropertyTag RenderingPosition = new TnefPropertyTag(TnefPropertyId.RenderingPosition, TnefPropertyType.Long);

		// Token: 0x04000BB7 RID: 2999
		public static readonly TnefPropertyTag AttachTransportNameA = new TnefPropertyTag(TnefPropertyId.AttachTransportName, TnefPropertyType.String8);

		// Token: 0x04000BB8 RID: 3000
		public static readonly TnefPropertyTag AttachTransportNameW = new TnefPropertyTag(TnefPropertyId.AttachTransportName, TnefPropertyType.Unicode);

		// Token: 0x04000BB9 RID: 3001
		public static readonly TnefPropertyTag AttachLongPathnameA = new TnefPropertyTag(TnefPropertyId.AttachLongPathname, TnefPropertyType.String8);

		// Token: 0x04000BBA RID: 3002
		public static readonly TnefPropertyTag AttachLongPathnameW = new TnefPropertyTag(TnefPropertyId.AttachLongPathname, TnefPropertyType.Unicode);

		// Token: 0x04000BBB RID: 3003
		public static readonly TnefPropertyTag AttachMimeTagA = new TnefPropertyTag(TnefPropertyId.AttachMimeTag, TnefPropertyType.String8);

		// Token: 0x04000BBC RID: 3004
		public static readonly TnefPropertyTag AttachMimeTagW = new TnefPropertyTag(TnefPropertyId.AttachMimeTag, TnefPropertyType.Unicode);

		// Token: 0x04000BBD RID: 3005
		public static readonly TnefPropertyTag AttachAdditionalInfo = new TnefPropertyTag(TnefPropertyId.AttachAdditionalInfo, TnefPropertyType.Binary);

		// Token: 0x04000BBE RID: 3006
		public static readonly TnefPropertyTag AttachMimeSequence = new TnefPropertyTag(TnefPropertyId.AttachMimeSequence, TnefPropertyType.Long);

		// Token: 0x04000BBF RID: 3007
		public static readonly TnefPropertyTag AttachContentBaseA = new TnefPropertyTag(TnefPropertyId.AttachContentBase, TnefPropertyType.String8);

		// Token: 0x04000BC0 RID: 3008
		public static readonly TnefPropertyTag AttachContentBaseW = new TnefPropertyTag(TnefPropertyId.AttachContentBase, TnefPropertyType.Unicode);

		// Token: 0x04000BC1 RID: 3009
		public static readonly TnefPropertyTag AttachContentIdA = new TnefPropertyTag(TnefPropertyId.AttachContentId, TnefPropertyType.String8);

		// Token: 0x04000BC2 RID: 3010
		public static readonly TnefPropertyTag AttachContentIdW = new TnefPropertyTag(TnefPropertyId.AttachContentId, TnefPropertyType.Unicode);

		// Token: 0x04000BC3 RID: 3011
		public static readonly TnefPropertyTag AttachContentLocationA = new TnefPropertyTag(TnefPropertyId.AttachContentLocation, TnefPropertyType.String8);

		// Token: 0x04000BC4 RID: 3012
		public static readonly TnefPropertyTag AttachContentLocationW = new TnefPropertyTag(TnefPropertyId.AttachContentLocation, TnefPropertyType.Unicode);

		// Token: 0x04000BC5 RID: 3013
		public static readonly TnefPropertyTag AttachFlags = new TnefPropertyTag(TnefPropertyId.AttachFlags, TnefPropertyType.Long);

		// Token: 0x04000BC6 RID: 3014
		public static readonly TnefPropertyTag AttachNetscapeMacInfo = new TnefPropertyTag(TnefPropertyId.AttachNetscapeMacInfo, TnefPropertyType.Binary);

		// Token: 0x04000BC7 RID: 3015
		public static readonly TnefPropertyTag AttachDispositionA = new TnefPropertyTag(TnefPropertyId.AttachDisposition, TnefPropertyType.String8);

		// Token: 0x04000BC8 RID: 3016
		public static readonly TnefPropertyTag AttachDispositionW = new TnefPropertyTag(TnefPropertyId.AttachDisposition, TnefPropertyType.Unicode);

		// Token: 0x04000BC9 RID: 3017
		public static readonly TnefPropertyTag AttachHidden = new TnefPropertyTag(TnefPropertyId.AttachHidden, TnefPropertyType.Boolean);

		// Token: 0x04000BCA RID: 3018
		public static readonly TnefPropertyTag AttachmentFlags = new TnefPropertyTag(TnefPropertyId.AttachmentFlags, TnefPropertyType.Long);

		// Token: 0x04000BCB RID: 3019
		public static readonly TnefPropertyTag LockBranchId = new TnefPropertyTag(TnefPropertyId.LockBranchId, TnefPropertyType.I8);

		// Token: 0x04000BCC RID: 3020
		public static readonly TnefPropertyTag LockResourceFid = new TnefPropertyTag(TnefPropertyId.LockResourceFid, TnefPropertyType.I8);

		// Token: 0x04000BCD RID: 3021
		public static readonly TnefPropertyTag LockResourceDid = new TnefPropertyTag(TnefPropertyId.LockResourceDid, TnefPropertyType.I8);

		// Token: 0x04000BCE RID: 3022
		public static readonly TnefPropertyTag LockResourceMid = new TnefPropertyTag(TnefPropertyId.LockResourceMid, TnefPropertyType.I8);

		// Token: 0x04000BCF RID: 3023
		public static readonly TnefPropertyTag LockEnlistmentContext = new TnefPropertyTag(TnefPropertyId.LockEnlistmentContext, TnefPropertyType.Binary);

		// Token: 0x04000BD0 RID: 3024
		public static readonly TnefPropertyTag LockType = new TnefPropertyTag(TnefPropertyId.LockType, TnefPropertyType.I2);

		// Token: 0x04000BD1 RID: 3025
		public static readonly TnefPropertyTag LockScope = new TnefPropertyTag(TnefPropertyId.LockScope, TnefPropertyType.I2);

		// Token: 0x04000BD2 RID: 3026
		public static readonly TnefPropertyTag LockPersistent = new TnefPropertyTag(TnefPropertyId.LockPersistent, TnefPropertyType.Boolean);

		// Token: 0x04000BD3 RID: 3027
		public static readonly TnefPropertyTag LockDepth = new TnefPropertyTag(TnefPropertyId.LockDepth, TnefPropertyType.Long);

		// Token: 0x04000BD4 RID: 3028
		public static readonly TnefPropertyTag LockTimeout = new TnefPropertyTag(TnefPropertyId.LockTimeout, TnefPropertyType.Long);

		// Token: 0x04000BD5 RID: 3029
		public static readonly TnefPropertyTag LockExpiryTime = new TnefPropertyTag(TnefPropertyId.LockExpiryTime, TnefPropertyType.SysTime);

		// Token: 0x04000BD6 RID: 3030
		public static readonly TnefPropertyTag DisplayType = new TnefPropertyTag(TnefPropertyId.DisplayType, TnefPropertyType.Long);

		// Token: 0x04000BD7 RID: 3031
		public static readonly TnefPropertyTag Templateid = new TnefPropertyTag(TnefPropertyId.Templateid, TnefPropertyType.Binary);

		// Token: 0x04000BD8 RID: 3032
		public static readonly TnefPropertyTag PrimaryCapability = new TnefPropertyTag(TnefPropertyId.PrimaryCapability, TnefPropertyType.Binary);

		// Token: 0x04000BD9 RID: 3033
		public static readonly TnefPropertyTag SmtpAddressA = new TnefPropertyTag(TnefPropertyId.SmtpAddress, TnefPropertyType.String8);

		// Token: 0x04000BDA RID: 3034
		public static readonly TnefPropertyTag SmtpAddressW = new TnefPropertyTag(TnefPropertyId.SmtpAddress, TnefPropertyType.Unicode);

		// Token: 0x04000BDB RID: 3035
		public static readonly TnefPropertyTag SevenBitDisplayName = new TnefPropertyTag(TnefPropertyId.SevenBitDisplayName, TnefPropertyType.String8);

		// Token: 0x04000BDC RID: 3036
		public static readonly TnefPropertyTag AccountA = new TnefPropertyTag(TnefPropertyId.Account, TnefPropertyType.String8);

		// Token: 0x04000BDD RID: 3037
		public static readonly TnefPropertyTag AccountW = new TnefPropertyTag(TnefPropertyId.Account, TnefPropertyType.Unicode);

		// Token: 0x04000BDE RID: 3038
		public static readonly TnefPropertyTag AlternateRecipient = new TnefPropertyTag(TnefPropertyId.AlternateRecipient, TnefPropertyType.Binary);

		// Token: 0x04000BDF RID: 3039
		public static readonly TnefPropertyTag CallbackTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.CallbackTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000BE0 RID: 3040
		public static readonly TnefPropertyTag CallbackTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.CallbackTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000BE1 RID: 3041
		public static readonly TnefPropertyTag ConversionProhibited = new TnefPropertyTag(TnefPropertyId.ConversionProhibited, TnefPropertyType.Boolean);

		// Token: 0x04000BE2 RID: 3042
		public static readonly TnefPropertyTag DiscloseRecipients = new TnefPropertyTag(TnefPropertyId.DiscloseRecipients, TnefPropertyType.Boolean);

		// Token: 0x04000BE3 RID: 3043
		public static readonly TnefPropertyTag GenerationA = new TnefPropertyTag(TnefPropertyId.Generation, TnefPropertyType.String8);

		// Token: 0x04000BE4 RID: 3044
		public static readonly TnefPropertyTag GenerationW = new TnefPropertyTag(TnefPropertyId.Generation, TnefPropertyType.Unicode);

		// Token: 0x04000BE5 RID: 3045
		public static readonly TnefPropertyTag GivenNameA = new TnefPropertyTag(TnefPropertyId.GivenName, TnefPropertyType.String8);

		// Token: 0x04000BE6 RID: 3046
		public static readonly TnefPropertyTag GivenNameW = new TnefPropertyTag(TnefPropertyId.GivenName, TnefPropertyType.Unicode);

		// Token: 0x04000BE7 RID: 3047
		public static readonly TnefPropertyTag GovernmentIdNumberA = new TnefPropertyTag(TnefPropertyId.GovernmentIdNumber, TnefPropertyType.String8);

		// Token: 0x04000BE8 RID: 3048
		public static readonly TnefPropertyTag GovernmentIdNumberW = new TnefPropertyTag(TnefPropertyId.GovernmentIdNumber, TnefPropertyType.Unicode);

		// Token: 0x04000BE9 RID: 3049
		public static readonly TnefPropertyTag OfficeTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.OfficeTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000BEA RID: 3050
		public static readonly TnefPropertyTag OfficeTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.OfficeTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000BEB RID: 3051
		public static readonly TnefPropertyTag HomeTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.HomeTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000BEC RID: 3052
		public static readonly TnefPropertyTag HomeTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.HomeTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000BED RID: 3053
		public static readonly TnefPropertyTag InitialsA = new TnefPropertyTag(TnefPropertyId.Initials, TnefPropertyType.String8);

		// Token: 0x04000BEE RID: 3054
		public static readonly TnefPropertyTag InitialsW = new TnefPropertyTag(TnefPropertyId.Initials, TnefPropertyType.Unicode);

		// Token: 0x04000BEF RID: 3055
		public static readonly TnefPropertyTag KeywordA = new TnefPropertyTag(TnefPropertyId.Keyword, TnefPropertyType.String8);

		// Token: 0x04000BF0 RID: 3056
		public static readonly TnefPropertyTag KeywordW = new TnefPropertyTag(TnefPropertyId.Keyword, TnefPropertyType.Unicode);

		// Token: 0x04000BF1 RID: 3057
		public static readonly TnefPropertyTag LanguageA = new TnefPropertyTag(TnefPropertyId.Language, TnefPropertyType.String8);

		// Token: 0x04000BF2 RID: 3058
		public static readonly TnefPropertyTag LanguageW = new TnefPropertyTag(TnefPropertyId.Language, TnefPropertyType.Unicode);

		// Token: 0x04000BF3 RID: 3059
		public static readonly TnefPropertyTag LocationA = new TnefPropertyTag(TnefPropertyId.Location, TnefPropertyType.String8);

		// Token: 0x04000BF4 RID: 3060
		public static readonly TnefPropertyTag LocationW = new TnefPropertyTag(TnefPropertyId.Location, TnefPropertyType.Unicode);

		// Token: 0x04000BF5 RID: 3061
		public static readonly TnefPropertyTag MailPermission = new TnefPropertyTag(TnefPropertyId.MailPermission, TnefPropertyType.Boolean);

		// Token: 0x04000BF6 RID: 3062
		public static readonly TnefPropertyTag MhsCommonNameA = new TnefPropertyTag(TnefPropertyId.MhsCommonName, TnefPropertyType.String8);

		// Token: 0x04000BF7 RID: 3063
		public static readonly TnefPropertyTag MhsCommonNameW = new TnefPropertyTag(TnefPropertyId.MhsCommonName, TnefPropertyType.Unicode);

		// Token: 0x04000BF8 RID: 3064
		public static readonly TnefPropertyTag OrganizationalIdNumberA = new TnefPropertyTag(TnefPropertyId.OrganizationalIdNumber, TnefPropertyType.String8);

		// Token: 0x04000BF9 RID: 3065
		public static readonly TnefPropertyTag OrganizationalIdNumberW = new TnefPropertyTag(TnefPropertyId.OrganizationalIdNumber, TnefPropertyType.Unicode);

		// Token: 0x04000BFA RID: 3066
		public static readonly TnefPropertyTag SurnameA = new TnefPropertyTag(TnefPropertyId.Surname, TnefPropertyType.String8);

		// Token: 0x04000BFB RID: 3067
		public static readonly TnefPropertyTag SurnameW = new TnefPropertyTag(TnefPropertyId.Surname, TnefPropertyType.Unicode);

		// Token: 0x04000BFC RID: 3068
		public static readonly TnefPropertyTag OriginalEntryId = new TnefPropertyTag(TnefPropertyId.OriginalEntryId, TnefPropertyType.Binary);

		// Token: 0x04000BFD RID: 3069
		public static readonly TnefPropertyTag OriginalDisplayNameA = new TnefPropertyTag(TnefPropertyId.OriginalDisplayName, TnefPropertyType.String8);

		// Token: 0x04000BFE RID: 3070
		public static readonly TnefPropertyTag OriginalDisplayNameW = new TnefPropertyTag(TnefPropertyId.OriginalDisplayName, TnefPropertyType.Unicode);

		// Token: 0x04000BFF RID: 3071
		public static readonly TnefPropertyTag OriginalSearchKey = new TnefPropertyTag(TnefPropertyId.OriginalSearchKey, TnefPropertyType.Binary);

		// Token: 0x04000C00 RID: 3072
		public static readonly TnefPropertyTag PostalAddressA = new TnefPropertyTag(TnefPropertyId.PostalAddress, TnefPropertyType.String8);

		// Token: 0x04000C01 RID: 3073
		public static readonly TnefPropertyTag PostalAddressW = new TnefPropertyTag(TnefPropertyId.PostalAddress, TnefPropertyType.Unicode);

		// Token: 0x04000C02 RID: 3074
		public static readonly TnefPropertyTag CompanyNameA = new TnefPropertyTag(TnefPropertyId.CompanyName, TnefPropertyType.String8);

		// Token: 0x04000C03 RID: 3075
		public static readonly TnefPropertyTag CompanyNameW = new TnefPropertyTag(TnefPropertyId.CompanyName, TnefPropertyType.Unicode);

		// Token: 0x04000C04 RID: 3076
		public static readonly TnefPropertyTag TitleA = new TnefPropertyTag(TnefPropertyId.Title, TnefPropertyType.String8);

		// Token: 0x04000C05 RID: 3077
		public static readonly TnefPropertyTag TitleW = new TnefPropertyTag(TnefPropertyId.Title, TnefPropertyType.Unicode);

		// Token: 0x04000C06 RID: 3078
		public static readonly TnefPropertyTag DepartmentNameA = new TnefPropertyTag(TnefPropertyId.DepartmentName, TnefPropertyType.String8);

		// Token: 0x04000C07 RID: 3079
		public static readonly TnefPropertyTag DepartmentNameW = new TnefPropertyTag(TnefPropertyId.DepartmentName, TnefPropertyType.Unicode);

		// Token: 0x04000C08 RID: 3080
		public static readonly TnefPropertyTag OfficeLocationA = new TnefPropertyTag(TnefPropertyId.OfficeLocation, TnefPropertyType.String8);

		// Token: 0x04000C09 RID: 3081
		public static readonly TnefPropertyTag OfficeLocationW = new TnefPropertyTag(TnefPropertyId.OfficeLocation, TnefPropertyType.Unicode);

		// Token: 0x04000C0A RID: 3082
		public static readonly TnefPropertyTag PrimaryTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.PrimaryTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C0B RID: 3083
		public static readonly TnefPropertyTag PrimaryTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.PrimaryTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C0C RID: 3084
		public static readonly TnefPropertyTag Office2TelephoneNumberA = new TnefPropertyTag(TnefPropertyId.Office2TelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C0D RID: 3085
		public static readonly TnefPropertyTag Office2TelephoneNumberW = new TnefPropertyTag(TnefPropertyId.Office2TelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C0E RID: 3086
		public static readonly TnefPropertyTag Business2TelephoneNumberA = new TnefPropertyTag(TnefPropertyId.Office2TelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C0F RID: 3087
		public static readonly TnefPropertyTag Business2TelephoneNumberW = new TnefPropertyTag(TnefPropertyId.Office2TelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C10 RID: 3088
		public static readonly TnefPropertyTag Business2TelephoneNumberAMv = new TnefPropertyTag(TnefPropertyId.Office2TelephoneNumber, (TnefPropertyType)4126);

		// Token: 0x04000C11 RID: 3089
		public static readonly TnefPropertyTag Business2TelephoneNumberWMv = new TnefPropertyTag(TnefPropertyId.Office2TelephoneNumber, (TnefPropertyType)4127);

		// Token: 0x04000C12 RID: 3090
		public static readonly TnefPropertyTag MobileTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.MobileTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C13 RID: 3091
		public static readonly TnefPropertyTag MobileTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.MobileTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C14 RID: 3092
		public static readonly TnefPropertyTag RadioTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.RadioTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C15 RID: 3093
		public static readonly TnefPropertyTag RadioTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.RadioTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C16 RID: 3094
		public static readonly TnefPropertyTag CarTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.CarTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C17 RID: 3095
		public static readonly TnefPropertyTag CarTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.CarTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C18 RID: 3096
		public static readonly TnefPropertyTag OtherTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.OtherTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C19 RID: 3097
		public static readonly TnefPropertyTag OtherTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.OtherTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C1A RID: 3098
		public static readonly TnefPropertyTag TransmitableDisplayNameA = new TnefPropertyTag(TnefPropertyId.TransmitableDisplayName, TnefPropertyType.String8);

		// Token: 0x04000C1B RID: 3099
		public static readonly TnefPropertyTag TransmitableDisplayNameW = new TnefPropertyTag(TnefPropertyId.TransmitableDisplayName, TnefPropertyType.Unicode);

		// Token: 0x04000C1C RID: 3100
		public static readonly TnefPropertyTag PagerTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.PagerTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C1D RID: 3101
		public static readonly TnefPropertyTag PagerTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.PagerTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C1E RID: 3102
		public static readonly TnefPropertyTag BeeperTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.PagerTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C1F RID: 3103
		public static readonly TnefPropertyTag BeeperTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.PagerTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C20 RID: 3104
		public static readonly TnefPropertyTag UserCertificate = new TnefPropertyTag(TnefPropertyId.UserCertificate, TnefPropertyType.Binary);

		// Token: 0x04000C21 RID: 3105
		public static readonly TnefPropertyTag PrimaryFaxNumberA = new TnefPropertyTag(TnefPropertyId.PrimaryFaxNumber, TnefPropertyType.String8);

		// Token: 0x04000C22 RID: 3106
		public static readonly TnefPropertyTag PrimaryFaxNumberW = new TnefPropertyTag(TnefPropertyId.PrimaryFaxNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C23 RID: 3107
		public static readonly TnefPropertyTag BusinessFaxNumberA = new TnefPropertyTag(TnefPropertyId.BusinessFaxNumber, TnefPropertyType.String8);

		// Token: 0x04000C24 RID: 3108
		public static readonly TnefPropertyTag BusinessFaxNumberW = new TnefPropertyTag(TnefPropertyId.BusinessFaxNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C25 RID: 3109
		public static readonly TnefPropertyTag HomeFaxNumberA = new TnefPropertyTag(TnefPropertyId.HomeFaxNumber, TnefPropertyType.String8);

		// Token: 0x04000C26 RID: 3110
		public static readonly TnefPropertyTag HomeFaxNumberW = new TnefPropertyTag(TnefPropertyId.HomeFaxNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C27 RID: 3111
		public static readonly TnefPropertyTag BusinessAddressCountryA = new TnefPropertyTag(TnefPropertyId.BusinessAddressCountry, TnefPropertyType.String8);

		// Token: 0x04000C28 RID: 3112
		public static readonly TnefPropertyTag BusinessAddressCountryW = new TnefPropertyTag(TnefPropertyId.BusinessAddressCountry, TnefPropertyType.Unicode);

		// Token: 0x04000C29 RID: 3113
		public static readonly TnefPropertyTag CountryW = new TnefPropertyTag(TnefPropertyId.BusinessAddressCountry, TnefPropertyType.Unicode);

		// Token: 0x04000C2A RID: 3114
		public static readonly TnefPropertyTag CountryA = new TnefPropertyTag(TnefPropertyId.BusinessAddressCountry, TnefPropertyType.String8);

		// Token: 0x04000C2B RID: 3115
		public static readonly TnefPropertyTag BusinessAddressCityW = new TnefPropertyTag(TnefPropertyId.BusinessAddressCity, TnefPropertyType.Unicode);

		// Token: 0x04000C2C RID: 3116
		public static readonly TnefPropertyTag BusinessAddressCityA = new TnefPropertyTag(TnefPropertyId.BusinessAddressCity, TnefPropertyType.String8);

		// Token: 0x04000C2D RID: 3117
		public static readonly TnefPropertyTag LocalityW = new TnefPropertyTag(TnefPropertyId.BusinessAddressCity, TnefPropertyType.Unicode);

		// Token: 0x04000C2E RID: 3118
		public static readonly TnefPropertyTag LocalityA = new TnefPropertyTag(TnefPropertyId.BusinessAddressCity, TnefPropertyType.String8);

		// Token: 0x04000C2F RID: 3119
		public static readonly TnefPropertyTag StateOrProvinceA = new TnefPropertyTag(TnefPropertyId.StateOrProvince, TnefPropertyType.String8);

		// Token: 0x04000C30 RID: 3120
		public static readonly TnefPropertyTag StateOrProvinceW = new TnefPropertyTag(TnefPropertyId.StateOrProvince, TnefPropertyType.Unicode);

		// Token: 0x04000C31 RID: 3121
		public static readonly TnefPropertyTag BusinessAddressStreetA = new TnefPropertyTag(TnefPropertyId.BusinessAddressStreet, TnefPropertyType.String8);

		// Token: 0x04000C32 RID: 3122
		public static readonly TnefPropertyTag BusinessAddressStreetW = new TnefPropertyTag(TnefPropertyId.BusinessAddressStreet, TnefPropertyType.Unicode);

		// Token: 0x04000C33 RID: 3123
		public static readonly TnefPropertyTag StreetAddressW = new TnefPropertyTag(TnefPropertyId.BusinessAddressStreet, TnefPropertyType.Unicode);

		// Token: 0x04000C34 RID: 3124
		public static readonly TnefPropertyTag StreetAddressA = new TnefPropertyTag(TnefPropertyId.BusinessAddressStreet, TnefPropertyType.String8);

		// Token: 0x04000C35 RID: 3125
		public static readonly TnefPropertyTag PostalCodeA = new TnefPropertyTag(TnefPropertyId.PostalCode, TnefPropertyType.String8);

		// Token: 0x04000C36 RID: 3126
		public static readonly TnefPropertyTag PostalCodeW = new TnefPropertyTag(TnefPropertyId.PostalCode, TnefPropertyType.Unicode);

		// Token: 0x04000C37 RID: 3127
		public static readonly TnefPropertyTag BusinessAddressPostalCodeW = new TnefPropertyTag(TnefPropertyId.PostalCode, TnefPropertyType.Unicode);

		// Token: 0x04000C38 RID: 3128
		public static readonly TnefPropertyTag BusinessAddressPostalCodeA = new TnefPropertyTag(TnefPropertyId.PostalCode, TnefPropertyType.String8);

		// Token: 0x04000C39 RID: 3129
		public static readonly TnefPropertyTag PostOfficeBoxA = new TnefPropertyTag(TnefPropertyId.PostOfficeBox, TnefPropertyType.String8);

		// Token: 0x04000C3A RID: 3130
		public static readonly TnefPropertyTag PostOfficeBoxW = new TnefPropertyTag(TnefPropertyId.PostOfficeBox, TnefPropertyType.Unicode);

		// Token: 0x04000C3B RID: 3131
		public static readonly TnefPropertyTag TelexNumberA = new TnefPropertyTag(TnefPropertyId.TelexNumber, TnefPropertyType.String8);

		// Token: 0x04000C3C RID: 3132
		public static readonly TnefPropertyTag TelexNumberW = new TnefPropertyTag(TnefPropertyId.TelexNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C3D RID: 3133
		public static readonly TnefPropertyTag IsdnNumberA = new TnefPropertyTag(TnefPropertyId.IsdnNumber, TnefPropertyType.String8);

		// Token: 0x04000C3E RID: 3134
		public static readonly TnefPropertyTag IsdnNumberW = new TnefPropertyTag(TnefPropertyId.IsdnNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C3F RID: 3135
		public static readonly TnefPropertyTag AssistantTelephoneNumberA = new TnefPropertyTag(TnefPropertyId.AssistantTelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C40 RID: 3136
		public static readonly TnefPropertyTag AssistantTelephoneNumberW = new TnefPropertyTag(TnefPropertyId.AssistantTelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C41 RID: 3137
		public static readonly TnefPropertyTag Home2TelephoneNumberA = new TnefPropertyTag(TnefPropertyId.Home2TelephoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C42 RID: 3138
		public static readonly TnefPropertyTag Home2TelephoneNumberW = new TnefPropertyTag(TnefPropertyId.Home2TelephoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C43 RID: 3139
		public static readonly TnefPropertyTag Home2TelephoneNumberAMv = new TnefPropertyTag(TnefPropertyId.Home2TelephoneNumber, (TnefPropertyType)4126);

		// Token: 0x04000C44 RID: 3140
		public static readonly TnefPropertyTag Home2TelephoneNumberWMv = new TnefPropertyTag(TnefPropertyId.Home2TelephoneNumber, (TnefPropertyType)4127);

		// Token: 0x04000C45 RID: 3141
		public static readonly TnefPropertyTag AssistantA = new TnefPropertyTag(TnefPropertyId.Assistant, TnefPropertyType.String8);

		// Token: 0x04000C46 RID: 3142
		public static readonly TnefPropertyTag AssistantW = new TnefPropertyTag(TnefPropertyId.Assistant, TnefPropertyType.Unicode);

		// Token: 0x04000C47 RID: 3143
		public static readonly TnefPropertyTag SendRichInfo = new TnefPropertyTag(TnefPropertyId.SendRichInfo, TnefPropertyType.Boolean);

		// Token: 0x04000C48 RID: 3144
		public static readonly TnefPropertyTag WeddingAnniversary = new TnefPropertyTag(TnefPropertyId.WeddingAnniversary, TnefPropertyType.SysTime);

		// Token: 0x04000C49 RID: 3145
		public static readonly TnefPropertyTag Birthday = new TnefPropertyTag(TnefPropertyId.Birthday, TnefPropertyType.SysTime);

		// Token: 0x04000C4A RID: 3146
		public static readonly TnefPropertyTag HobbiesA = new TnefPropertyTag(TnefPropertyId.Hobbies, TnefPropertyType.String8);

		// Token: 0x04000C4B RID: 3147
		public static readonly TnefPropertyTag HobbiesW = new TnefPropertyTag(TnefPropertyId.Hobbies, TnefPropertyType.Unicode);

		// Token: 0x04000C4C RID: 3148
		public static readonly TnefPropertyTag MiddleNameA = new TnefPropertyTag(TnefPropertyId.MiddleName, TnefPropertyType.String8);

		// Token: 0x04000C4D RID: 3149
		public static readonly TnefPropertyTag MiddleNameW = new TnefPropertyTag(TnefPropertyId.MiddleName, TnefPropertyType.Unicode);

		// Token: 0x04000C4E RID: 3150
		public static readonly TnefPropertyTag DisplayNamePrefixA = new TnefPropertyTag(TnefPropertyId.DisplayNamePrefix, TnefPropertyType.String8);

		// Token: 0x04000C4F RID: 3151
		public static readonly TnefPropertyTag DisplayNamePrefixW = new TnefPropertyTag(TnefPropertyId.DisplayNamePrefix, TnefPropertyType.Unicode);

		// Token: 0x04000C50 RID: 3152
		public static readonly TnefPropertyTag ProfessionA = new TnefPropertyTag(TnefPropertyId.Profession, TnefPropertyType.String8);

		// Token: 0x04000C51 RID: 3153
		public static readonly TnefPropertyTag ProfessionW = new TnefPropertyTag(TnefPropertyId.Profession, TnefPropertyType.Unicode);

		// Token: 0x04000C52 RID: 3154
		public static readonly TnefPropertyTag ReferredByNameA = new TnefPropertyTag(TnefPropertyId.ReferredByName, TnefPropertyType.String8);

		// Token: 0x04000C53 RID: 3155
		public static readonly TnefPropertyTag ReferredByNameW = new TnefPropertyTag(TnefPropertyId.ReferredByName, TnefPropertyType.Unicode);

		// Token: 0x04000C54 RID: 3156
		public static readonly TnefPropertyTag PreferredByNameW = new TnefPropertyTag(TnefPropertyId.ReferredByName, TnefPropertyType.Unicode);

		// Token: 0x04000C55 RID: 3157
		public static readonly TnefPropertyTag PreferredByNameA = new TnefPropertyTag(TnefPropertyId.ReferredByName, TnefPropertyType.String8);

		// Token: 0x04000C56 RID: 3158
		public static readonly TnefPropertyTag SpouseNameA = new TnefPropertyTag(TnefPropertyId.SpouseName, TnefPropertyType.String8);

		// Token: 0x04000C57 RID: 3159
		public static readonly TnefPropertyTag SpouseNameW = new TnefPropertyTag(TnefPropertyId.SpouseName, TnefPropertyType.Unicode);

		// Token: 0x04000C58 RID: 3160
		public static readonly TnefPropertyTag ComputerNetworkNameA = new TnefPropertyTag(TnefPropertyId.ComputerNetworkName, TnefPropertyType.String8);

		// Token: 0x04000C59 RID: 3161
		public static readonly TnefPropertyTag ComputerNetworkNameW = new TnefPropertyTag(TnefPropertyId.ComputerNetworkName, TnefPropertyType.Unicode);

		// Token: 0x04000C5A RID: 3162
		public static readonly TnefPropertyTag CustomerIdA = new TnefPropertyTag(TnefPropertyId.CustomerId, TnefPropertyType.String8);

		// Token: 0x04000C5B RID: 3163
		public static readonly TnefPropertyTag CustomerIdW = new TnefPropertyTag(TnefPropertyId.CustomerId, TnefPropertyType.Unicode);

		// Token: 0x04000C5C RID: 3164
		public static readonly TnefPropertyTag TtytddPhoneNumberA = new TnefPropertyTag(TnefPropertyId.TtytddPhoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C5D RID: 3165
		public static readonly TnefPropertyTag TtytddPhoneNumberW = new TnefPropertyTag(TnefPropertyId.TtytddPhoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C5E RID: 3166
		public static readonly TnefPropertyTag FtpSiteA = new TnefPropertyTag(TnefPropertyId.FtpSite, TnefPropertyType.String8);

		// Token: 0x04000C5F RID: 3167
		public static readonly TnefPropertyTag FtpSiteW = new TnefPropertyTag(TnefPropertyId.FtpSite, TnefPropertyType.Unicode);

		// Token: 0x04000C60 RID: 3168
		public static readonly TnefPropertyTag Gender = new TnefPropertyTag(TnefPropertyId.Gender, TnefPropertyType.I2);

		// Token: 0x04000C61 RID: 3169
		public static readonly TnefPropertyTag ManagerNameA = new TnefPropertyTag(TnefPropertyId.ManagerName, TnefPropertyType.String8);

		// Token: 0x04000C62 RID: 3170
		public static readonly TnefPropertyTag ManagerNameW = new TnefPropertyTag(TnefPropertyId.ManagerName, TnefPropertyType.Unicode);

		// Token: 0x04000C63 RID: 3171
		public static readonly TnefPropertyTag NicknameA = new TnefPropertyTag(TnefPropertyId.Nickname, TnefPropertyType.String8);

		// Token: 0x04000C64 RID: 3172
		public static readonly TnefPropertyTag NicknameW = new TnefPropertyTag(TnefPropertyId.Nickname, TnefPropertyType.Unicode);

		// Token: 0x04000C65 RID: 3173
		public static readonly TnefPropertyTag PersonalHomePageA = new TnefPropertyTag(TnefPropertyId.PersonalHomePage, TnefPropertyType.String8);

		// Token: 0x04000C66 RID: 3174
		public static readonly TnefPropertyTag PersonalHomePageW = new TnefPropertyTag(TnefPropertyId.PersonalHomePage, TnefPropertyType.Unicode);

		// Token: 0x04000C67 RID: 3175
		public static readonly TnefPropertyTag BusinessHomePageA = new TnefPropertyTag(TnefPropertyId.BusinessHomePage, TnefPropertyType.String8);

		// Token: 0x04000C68 RID: 3176
		public static readonly TnefPropertyTag BusinessHomePageW = new TnefPropertyTag(TnefPropertyId.BusinessHomePage, TnefPropertyType.Unicode);

		// Token: 0x04000C69 RID: 3177
		public static readonly TnefPropertyTag ContactVersion = new TnefPropertyTag(TnefPropertyId.ContactVersion, TnefPropertyType.ClassId);

		// Token: 0x04000C6A RID: 3178
		public static readonly TnefPropertyTag ContactEntryIds = new TnefPropertyTag(TnefPropertyId.ContactEntryIds, (TnefPropertyType)4354);

		// Token: 0x04000C6B RID: 3179
		public static readonly TnefPropertyTag ContactAddrtypesA = new TnefPropertyTag(TnefPropertyId.ContactAddrtypes, (TnefPropertyType)4126);

		// Token: 0x04000C6C RID: 3180
		public static readonly TnefPropertyTag ContactAddrtypesW = new TnefPropertyTag(TnefPropertyId.ContactAddrtypes, (TnefPropertyType)4127);

		// Token: 0x04000C6D RID: 3181
		public static readonly TnefPropertyTag ContactDefaultAddressIndex = new TnefPropertyTag(TnefPropertyId.ContactDefaultAddressIndex, TnefPropertyType.Long);

		// Token: 0x04000C6E RID: 3182
		public static readonly TnefPropertyTag ContactEmailAddressesA = new TnefPropertyTag(TnefPropertyId.ContactEmailAddresses, (TnefPropertyType)4126);

		// Token: 0x04000C6F RID: 3183
		public static readonly TnefPropertyTag ContactEmailAddressesW = new TnefPropertyTag(TnefPropertyId.ContactEmailAddresses, (TnefPropertyType)4127);

		// Token: 0x04000C70 RID: 3184
		public static readonly TnefPropertyTag CompanyMainPhoneNumberA = new TnefPropertyTag(TnefPropertyId.CompanyMainPhoneNumber, TnefPropertyType.String8);

		// Token: 0x04000C71 RID: 3185
		public static readonly TnefPropertyTag CompanyMainPhoneNumberW = new TnefPropertyTag(TnefPropertyId.CompanyMainPhoneNumber, TnefPropertyType.Unicode);

		// Token: 0x04000C72 RID: 3186
		public static readonly TnefPropertyTag ChildrensNamesA = new TnefPropertyTag(TnefPropertyId.ChildrensNames, (TnefPropertyType)4126);

		// Token: 0x04000C73 RID: 3187
		public static readonly TnefPropertyTag ChildrensNamesW = new TnefPropertyTag(TnefPropertyId.ChildrensNames, (TnefPropertyType)4127);

		// Token: 0x04000C74 RID: 3188
		public static readonly TnefPropertyTag HomeAddressCityA = new TnefPropertyTag(TnefPropertyId.HomeAddressCity, TnefPropertyType.String8);

		// Token: 0x04000C75 RID: 3189
		public static readonly TnefPropertyTag HomeAddressCityW = new TnefPropertyTag(TnefPropertyId.HomeAddressCity, TnefPropertyType.Unicode);

		// Token: 0x04000C76 RID: 3190
		public static readonly TnefPropertyTag HomeAddressCountryA = new TnefPropertyTag(TnefPropertyId.HomeAddressCountry, TnefPropertyType.String8);

		// Token: 0x04000C77 RID: 3191
		public static readonly TnefPropertyTag HomeAddressCountryW = new TnefPropertyTag(TnefPropertyId.HomeAddressCountry, TnefPropertyType.Unicode);

		// Token: 0x04000C78 RID: 3192
		public static readonly TnefPropertyTag HomeAddressPostalCodeA = new TnefPropertyTag(TnefPropertyId.HomeAddressPostalCode, TnefPropertyType.String8);

		// Token: 0x04000C79 RID: 3193
		public static readonly TnefPropertyTag HomeAddressPostalCodeW = new TnefPropertyTag(TnefPropertyId.HomeAddressPostalCode, TnefPropertyType.Unicode);

		// Token: 0x04000C7A RID: 3194
		public static readonly TnefPropertyTag HomeAddressStateOrProvinceA = new TnefPropertyTag(TnefPropertyId.HomeAddressStateOrProvince, TnefPropertyType.String8);

		// Token: 0x04000C7B RID: 3195
		public static readonly TnefPropertyTag HomeAddressStateOrProvinceW = new TnefPropertyTag(TnefPropertyId.HomeAddressStateOrProvince, TnefPropertyType.Unicode);

		// Token: 0x04000C7C RID: 3196
		public static readonly TnefPropertyTag HomeAddressStreetA = new TnefPropertyTag(TnefPropertyId.HomeAddressStreet, TnefPropertyType.String8);

		// Token: 0x04000C7D RID: 3197
		public static readonly TnefPropertyTag HomeAddressStreetW = new TnefPropertyTag(TnefPropertyId.HomeAddressStreet, TnefPropertyType.Unicode);

		// Token: 0x04000C7E RID: 3198
		public static readonly TnefPropertyTag HomeAddressPostOfficeBoxA = new TnefPropertyTag(TnefPropertyId.HomeAddressPostOfficeBox, TnefPropertyType.String8);

		// Token: 0x04000C7F RID: 3199
		public static readonly TnefPropertyTag HomeAddressPostOfficeBoxW = new TnefPropertyTag(TnefPropertyId.HomeAddressPostOfficeBox, TnefPropertyType.Unicode);

		// Token: 0x04000C80 RID: 3200
		public static readonly TnefPropertyTag OtherAddressCityA = new TnefPropertyTag(TnefPropertyId.OtherAddressCity, TnefPropertyType.String8);

		// Token: 0x04000C81 RID: 3201
		public static readonly TnefPropertyTag OtherAddressCityW = new TnefPropertyTag(TnefPropertyId.OtherAddressCity, TnefPropertyType.Unicode);

		// Token: 0x04000C82 RID: 3202
		public static readonly TnefPropertyTag OtherAddressCountryA = new TnefPropertyTag(TnefPropertyId.OtherAddressCountry, TnefPropertyType.String8);

		// Token: 0x04000C83 RID: 3203
		public static readonly TnefPropertyTag OtherAddressCountryW = new TnefPropertyTag(TnefPropertyId.OtherAddressCountry, TnefPropertyType.Unicode);

		// Token: 0x04000C84 RID: 3204
		public static readonly TnefPropertyTag OtherAddressPostalCodeA = new TnefPropertyTag(TnefPropertyId.OtherAddressPostalCode, TnefPropertyType.String8);

		// Token: 0x04000C85 RID: 3205
		public static readonly TnefPropertyTag OtherAddressPostalCodeW = new TnefPropertyTag(TnefPropertyId.OtherAddressPostalCode, TnefPropertyType.Unicode);

		// Token: 0x04000C86 RID: 3206
		public static readonly TnefPropertyTag OtherAddressStateOrProvinceA = new TnefPropertyTag(TnefPropertyId.OtherAddressStateOrProvince, TnefPropertyType.String8);

		// Token: 0x04000C87 RID: 3207
		public static readonly TnefPropertyTag OtherAddressStateOrProvinceW = new TnefPropertyTag(TnefPropertyId.OtherAddressStateOrProvince, TnefPropertyType.Unicode);

		// Token: 0x04000C88 RID: 3208
		public static readonly TnefPropertyTag OtherAddressStreetA = new TnefPropertyTag(TnefPropertyId.OtherAddressStreet, TnefPropertyType.String8);

		// Token: 0x04000C89 RID: 3209
		public static readonly TnefPropertyTag OtherAddressStreetW = new TnefPropertyTag(TnefPropertyId.OtherAddressStreet, TnefPropertyType.Unicode);

		// Token: 0x04000C8A RID: 3210
		public static readonly TnefPropertyTag OtherAddressPostOfficeBoxA = new TnefPropertyTag(TnefPropertyId.OtherAddressPostOfficeBox, TnefPropertyType.String8);

		// Token: 0x04000C8B RID: 3211
		public static readonly TnefPropertyTag OtherAddressPostOfficeBoxW = new TnefPropertyTag(TnefPropertyId.OtherAddressPostOfficeBox, TnefPropertyType.Unicode);

		// Token: 0x04000C8C RID: 3212
		public static readonly TnefPropertyTag UserX509Certificate = new TnefPropertyTag(TnefPropertyId.UserX509Certificate, (TnefPropertyType)4354);

		// Token: 0x04000C8D RID: 3213
		public static readonly TnefPropertyTag SendInternetEncoding = new TnefPropertyTag(TnefPropertyId.SendInternetEncoding, TnefPropertyType.Long);

		// Token: 0x04000C8E RID: 3214
		public static readonly TnefPropertyTag StoreProviders = new TnefPropertyTag(TnefPropertyId.StoreProviders, TnefPropertyType.Binary);

		// Token: 0x04000C8F RID: 3215
		public static readonly TnefPropertyTag AbProviders = new TnefPropertyTag(TnefPropertyId.AbProviders, TnefPropertyType.Binary);

		// Token: 0x04000C90 RID: 3216
		public static readonly TnefPropertyTag TransportProviders = new TnefPropertyTag(TnefPropertyId.TransportProviders, TnefPropertyType.Binary);

		// Token: 0x04000C91 RID: 3217
		public static readonly TnefPropertyTag DefaultProfile = new TnefPropertyTag(TnefPropertyId.DefaultProfile, TnefPropertyType.Boolean);

		// Token: 0x04000C92 RID: 3218
		public static readonly TnefPropertyTag AbSearchPath = new TnefPropertyTag(TnefPropertyId.AbSearchPath, (TnefPropertyType)4354);

		// Token: 0x04000C93 RID: 3219
		public static readonly TnefPropertyTag AbDefaultDir = new TnefPropertyTag(TnefPropertyId.AbDefaultDir, TnefPropertyType.Binary);

		// Token: 0x04000C94 RID: 3220
		public static readonly TnefPropertyTag AbDefaultPab = new TnefPropertyTag(TnefPropertyId.AbDefaultPab, TnefPropertyType.Binary);

		// Token: 0x04000C95 RID: 3221
		public static readonly TnefPropertyTag FilteringHooks = new TnefPropertyTag(TnefPropertyId.FilteringHooks, TnefPropertyType.Binary);

		// Token: 0x04000C96 RID: 3222
		public static readonly TnefPropertyTag ServiceNameA = new TnefPropertyTag(TnefPropertyId.ServiceName, TnefPropertyType.String8);

		// Token: 0x04000C97 RID: 3223
		public static readonly TnefPropertyTag ServiceNameW = new TnefPropertyTag(TnefPropertyId.ServiceName, TnefPropertyType.Unicode);

		// Token: 0x04000C98 RID: 3224
		public static readonly TnefPropertyTag ServiceDllNameA = new TnefPropertyTag(TnefPropertyId.ServiceDllName, TnefPropertyType.String8);

		// Token: 0x04000C99 RID: 3225
		public static readonly TnefPropertyTag ServiceDllNameW = new TnefPropertyTag(TnefPropertyId.ServiceDllName, TnefPropertyType.Unicode);

		// Token: 0x04000C9A RID: 3226
		public static readonly TnefPropertyTag ServiceEntryName = new TnefPropertyTag(TnefPropertyId.ServiceEntryName, TnefPropertyType.String8);

		// Token: 0x04000C9B RID: 3227
		public static readonly TnefPropertyTag ServiceUid = new TnefPropertyTag(TnefPropertyId.ServiceUid, TnefPropertyType.Binary);

		// Token: 0x04000C9C RID: 3228
		public static readonly TnefPropertyTag ServiceExtraUids = new TnefPropertyTag(TnefPropertyId.ServiceExtraUids, TnefPropertyType.Binary);

		// Token: 0x04000C9D RID: 3229
		public static readonly TnefPropertyTag Services = new TnefPropertyTag(TnefPropertyId.Services, TnefPropertyType.Binary);

		// Token: 0x04000C9E RID: 3230
		public static readonly TnefPropertyTag ServiceSupportFilesA = new TnefPropertyTag(TnefPropertyId.ServiceSupportFiles, (TnefPropertyType)4126);

		// Token: 0x04000C9F RID: 3231
		public static readonly TnefPropertyTag ServiceSupportFilesW = new TnefPropertyTag(TnefPropertyId.ServiceSupportFiles, (TnefPropertyType)4127);

		// Token: 0x04000CA0 RID: 3232
		public static readonly TnefPropertyTag ServiceDeleteFilesA = new TnefPropertyTag(TnefPropertyId.ServiceDeleteFiles, (TnefPropertyType)4126);

		// Token: 0x04000CA1 RID: 3233
		public static readonly TnefPropertyTag ServiceDeleteFilesW = new TnefPropertyTag(TnefPropertyId.ServiceDeleteFiles, (TnefPropertyType)4127);

		// Token: 0x04000CA2 RID: 3234
		public static readonly TnefPropertyTag AbSearchPathUpdate = new TnefPropertyTag(TnefPropertyId.AbSearchPathUpdate, TnefPropertyType.Binary);

		// Token: 0x04000CA3 RID: 3235
		public static readonly TnefPropertyTag ProfileNameA = new TnefPropertyTag(TnefPropertyId.ProfileName, TnefPropertyType.String8);

		// Token: 0x04000CA4 RID: 3236
		public static readonly TnefPropertyTag ProfileNameW = new TnefPropertyTag(TnefPropertyId.ProfileName, TnefPropertyType.Unicode);

		// Token: 0x04000CA5 RID: 3237
		public static readonly TnefPropertyTag IdentityDisplayA = new TnefPropertyTag(TnefPropertyId.IdentityDisplay, TnefPropertyType.String8);

		// Token: 0x04000CA6 RID: 3238
		public static readonly TnefPropertyTag IdentityDisplayW = new TnefPropertyTag(TnefPropertyId.IdentityDisplay, TnefPropertyType.Unicode);

		// Token: 0x04000CA7 RID: 3239
		public static readonly TnefPropertyTag IdentityEntryId = new TnefPropertyTag(TnefPropertyId.IdentityEntryId, TnefPropertyType.Binary);

		// Token: 0x04000CA8 RID: 3240
		public static readonly TnefPropertyTag ResourceMethods = new TnefPropertyTag(TnefPropertyId.ResourceMethods, TnefPropertyType.Long);

		// Token: 0x04000CA9 RID: 3241
		public static readonly TnefPropertyTag ResourceType = new TnefPropertyTag(TnefPropertyId.ResourceType, TnefPropertyType.Long);

		// Token: 0x04000CAA RID: 3242
		public static readonly TnefPropertyTag StatusCode = new TnefPropertyTag(TnefPropertyId.StatusCode, TnefPropertyType.Long);

		// Token: 0x04000CAB RID: 3243
		public static readonly TnefPropertyTag IdentitySearchKey = new TnefPropertyTag(TnefPropertyId.IdentitySearchKey, TnefPropertyType.Binary);

		// Token: 0x04000CAC RID: 3244
		public static readonly TnefPropertyTag OwnStoreEntryId = new TnefPropertyTag(TnefPropertyId.OwnStoreEntryId, TnefPropertyType.Binary);

		// Token: 0x04000CAD RID: 3245
		public static readonly TnefPropertyTag ResourcePathA = new TnefPropertyTag(TnefPropertyId.ResourcePath, TnefPropertyType.String8);

		// Token: 0x04000CAE RID: 3246
		public static readonly TnefPropertyTag ResourcePathW = new TnefPropertyTag(TnefPropertyId.ResourcePath, TnefPropertyType.Unicode);

		// Token: 0x04000CAF RID: 3247
		public static readonly TnefPropertyTag StatusStringA = new TnefPropertyTag(TnefPropertyId.StatusString, TnefPropertyType.String8);

		// Token: 0x04000CB0 RID: 3248
		public static readonly TnefPropertyTag StatusStringW = new TnefPropertyTag(TnefPropertyId.StatusString, TnefPropertyType.Unicode);

		// Token: 0x04000CB1 RID: 3249
		public static readonly TnefPropertyTag X400DeferredDeliveryCancel = new TnefPropertyTag(TnefPropertyId.X400DeferredDeliveryCancel, TnefPropertyType.Boolean);

		// Token: 0x04000CB2 RID: 3250
		public static readonly TnefPropertyTag HeaderFolderEntryId = new TnefPropertyTag(TnefPropertyId.HeaderFolderEntryId, TnefPropertyType.Binary);

		// Token: 0x04000CB3 RID: 3251
		public static readonly TnefPropertyTag RemoteProgress = new TnefPropertyTag(TnefPropertyId.RemoteProgress, TnefPropertyType.Long);

		// Token: 0x04000CB4 RID: 3252
		public static readonly TnefPropertyTag RemoteProgressTextA = new TnefPropertyTag(TnefPropertyId.RemoteProgressText, TnefPropertyType.String8);

		// Token: 0x04000CB5 RID: 3253
		public static readonly TnefPropertyTag RemoteProgressTextW = new TnefPropertyTag(TnefPropertyId.RemoteProgressText, TnefPropertyType.Unicode);

		// Token: 0x04000CB6 RID: 3254
		public static readonly TnefPropertyTag RemoteValidateOk = new TnefPropertyTag(TnefPropertyId.RemoteValidateOk, TnefPropertyType.Boolean);

		// Token: 0x04000CB7 RID: 3255
		public static readonly TnefPropertyTag ControlFlags = new TnefPropertyTag(TnefPropertyId.ControlFlags, TnefPropertyType.Long);

		// Token: 0x04000CB8 RID: 3256
		public static readonly TnefPropertyTag ControlStructure = new TnefPropertyTag(TnefPropertyId.ControlStructure, TnefPropertyType.Binary);

		// Token: 0x04000CB9 RID: 3257
		public static readonly TnefPropertyTag ControlType = new TnefPropertyTag(TnefPropertyId.ControlType, TnefPropertyType.Long);

		// Token: 0x04000CBA RID: 3258
		public static readonly TnefPropertyTag Deltax = new TnefPropertyTag(TnefPropertyId.Deltax, TnefPropertyType.Long);

		// Token: 0x04000CBB RID: 3259
		public static readonly TnefPropertyTag Deltay = new TnefPropertyTag(TnefPropertyId.Deltay, TnefPropertyType.Long);

		// Token: 0x04000CBC RID: 3260
		public static readonly TnefPropertyTag Xpos = new TnefPropertyTag(TnefPropertyId.Xpos, TnefPropertyType.Long);

		// Token: 0x04000CBD RID: 3261
		public static readonly TnefPropertyTag Ypos = new TnefPropertyTag(TnefPropertyId.Ypos, TnefPropertyType.Long);

		// Token: 0x04000CBE RID: 3262
		public static readonly TnefPropertyTag ControlId = new TnefPropertyTag(TnefPropertyId.ControlId, TnefPropertyType.Binary);

		// Token: 0x04000CBF RID: 3263
		public static readonly TnefPropertyTag InitialDetailsPane = new TnefPropertyTag(TnefPropertyId.InitialDetailsPane, TnefPropertyType.Long);

		// Token: 0x04000CC0 RID: 3264
		public static readonly TnefPropertyTag InternetCPID = new TnefPropertyTag(TnefPropertyId.InternetCPID, TnefPropertyType.Long);

		// Token: 0x04000CC1 RID: 3265
		public static readonly TnefPropertyTag AutoResponseSuppress = new TnefPropertyTag(TnefPropertyId.AutoResponseSuppress, TnefPropertyType.Long);

		// Token: 0x04000CC2 RID: 3266
		public static readonly TnefPropertyTag MessageCodepage = new TnefPropertyTag(TnefPropertyId.MessageCodepage, TnefPropertyType.Long);

		// Token: 0x04000CC3 RID: 3267
		public static readonly TnefPropertyTag MessageLocaleID = new TnefPropertyTag(TnefPropertyId.MessageLocaleID, TnefPropertyType.Long);

		// Token: 0x04000CC4 RID: 3268
		public static readonly TnefPropertyTag OofReplyType = new TnefPropertyTag(TnefPropertyId.OofReplyType, TnefPropertyType.Long);

		// Token: 0x04000CC5 RID: 3269
		public static readonly TnefPropertyTag INetMailOverrideFormat = new TnefPropertyTag(TnefPropertyId.INetMailOverrideFormat, TnefPropertyType.Long);

		// Token: 0x04000CC6 RID: 3270
		public static readonly TnefPropertyTag INetMailOverrideCharset = new TnefPropertyTag(TnefPropertyId.INetMailOverrideCharset, TnefPropertyType.Unicode);

		// Token: 0x04000CC7 RID: 3271
		public static readonly TnefPropertyTag SendRecallReport = new TnefPropertyTag(TnefPropertyId.SendRecallReport, TnefPropertyType.Boolean);

		// Token: 0x04000CC8 RID: 3272
		internal static readonly TnefPropertyTag ReadReceiptDisplayNameA = new TnefPropertyTag((TnefPropertyId)16427, TnefPropertyType.String8);

		// Token: 0x04000CC9 RID: 3273
		internal static readonly TnefPropertyTag ReadReceiptDisplayNameW = new TnefPropertyTag((TnefPropertyId)16427, TnefPropertyType.Unicode);

		// Token: 0x04000CCA RID: 3274
		internal static readonly TnefPropertyTag ReadReceiptEmailAddressA = new TnefPropertyTag((TnefPropertyId)16426, TnefPropertyType.String8);

		// Token: 0x04000CCB RID: 3275
		internal static readonly TnefPropertyTag ReadReceiptEmailAddressW = new TnefPropertyTag((TnefPropertyId)16426, TnefPropertyType.Unicode);

		// Token: 0x04000CCC RID: 3276
		internal static readonly TnefPropertyTag ReadReceiptAddrtypeA = new TnefPropertyTag((TnefPropertyId)16425, TnefPropertyType.String8);

		// Token: 0x04000CCD RID: 3277
		internal static readonly TnefPropertyTag ReadReceiptAddrtypeW = new TnefPropertyTag((TnefPropertyId)16425, TnefPropertyType.Unicode);

		// Token: 0x04000CCE RID: 3278
		internal static readonly TnefPropertyTag ReadReceiptSmtpAddressA = new TnefPropertyTag((TnefPropertyId)3725, TnefPropertyType.String8);

		// Token: 0x04000CCF RID: 3279
		internal static readonly TnefPropertyTag ReadReceiptSmtpAddressW = new TnefPropertyTag((TnefPropertyId)3725, TnefPropertyType.Unicode);

		// Token: 0x04000CD0 RID: 3280
		private int tag;
	}
}
