using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000206 RID: 518
	internal enum PropTag : uint
	{
		// Token: 0x04000A03 RID: 2563
		Null,
		// Token: 0x04000A04 RID: 2564
		Unresolved = 10U,
		// Token: 0x04000A05 RID: 2565
		PstPath = 1728053278U,
		// Token: 0x04000A06 RID: 2566
		ProfileUser = 1711472670U,
		// Token: 0x04000A07 RID: 2567
		ProfileMailbox = 1711996958U,
		// Token: 0x04000A08 RID: 2568
		ProfileHomeServer = 1711407134U,
		// Token: 0x04000A09 RID: 2569
		ProfileHomeServerDn = 1712455710U,
		// Token: 0x04000A0A RID: 2570
		ProfileServer = 1712062494U,
		// Token: 0x04000A0B RID: 2571
		ProfileServerDn = 1712586782U,
		// Token: 0x04000A0C RID: 2572
		ProfileConfigFlags = 1711341571U,
		// Token: 0x04000A0D RID: 2573
		ProfileTransportFlags = 1711603715U,
		// Token: 0x04000A0E RID: 2574
		ProfileVersion = 1711276035U,
		// Token: 0x04000A0F RID: 2575
		ProfileConnectFlags = 1711538179U,
		// Token: 0x04000A10 RID: 2576
		ProfileUiState = 1711669251U,
		// Token: 0x04000A11 RID: 2577
		AcknowledgementMode = 65539U,
		// Token: 0x04000A12 RID: 2578
		AlternateRecipientAllowed = 131083U,
		// Token: 0x04000A13 RID: 2579
		AuthorizingUsers = 196866U,
		// Token: 0x04000A14 RID: 2580
		AutoForwardComment = 262175U,
		// Token: 0x04000A15 RID: 2581
		AutoForwarded = 327691U,
		// Token: 0x04000A16 RID: 2582
		ContentConfidentialityAlgorithmId = 393474U,
		// Token: 0x04000A17 RID: 2583
		ContentCorrelator = 459010U,
		// Token: 0x04000A18 RID: 2584
		ContentIdentifier = 524319U,
		// Token: 0x04000A19 RID: 2585
		ContentLength = 589827U,
		// Token: 0x04000A1A RID: 2586
		ContentReturnRequested = 655371U,
		// Token: 0x04000A1B RID: 2587
		ConversationKey = 721154U,
		// Token: 0x04000A1C RID: 2588
		ConversionEits = 786690U,
		// Token: 0x04000A1D RID: 2589
		ConversionWithLossProhibited = 851979U,
		// Token: 0x04000A1E RID: 2590
		ConvertedEits = 917762U,
		// Token: 0x04000A1F RID: 2591
		DeferredDeliveryTime = 983104U,
		// Token: 0x04000A20 RID: 2592
		DeliverTime = 1048640U,
		// Token: 0x04000A21 RID: 2593
		DiscardReason = 1114115U,
		// Token: 0x04000A22 RID: 2594
		DisclosureOfRecipients = 1179659U,
		// Token: 0x04000A23 RID: 2595
		DlExpansionHistory = 1245442U,
		// Token: 0x04000A24 RID: 2596
		DlExpansionProhibited = 1310731U,
		// Token: 0x04000A25 RID: 2597
		ExpiryTime = 1376320U,
		// Token: 0x04000A26 RID: 2598
		ImplicitConversionProhibited = 1441803U,
		// Token: 0x04000A27 RID: 2599
		Importance = 1507331U,
		// Token: 0x04000A28 RID: 2600
		IpmId = 1573122U,
		// Token: 0x04000A29 RID: 2601
		LatestDeliveryTime = 1638464U,
		// Token: 0x04000A2A RID: 2602
		MessageClass = 1703967U,
		// Token: 0x04000A2B RID: 2603
		MessageClassAnsi = 1703966U,
		// Token: 0x04000A2C RID: 2604
		MessageDeliveryId = 1769730U,
		// Token: 0x04000A2D RID: 2605
		MessageSecurityLabel = 1966338U,
		// Token: 0x04000A2E RID: 2606
		ObsoletedIpms = 2031874U,
		// Token: 0x04000A2F RID: 2607
		OriginallyIntendedRecipientName = 2097410U,
		// Token: 0x04000A30 RID: 2608
		OriginalEits = 2162946U,
		// Token: 0x04000A31 RID: 2609
		OriginatorCertificate = 2228482U,
		// Token: 0x04000A32 RID: 2610
		OriginatorDeliveryReportRequested = 2293771U,
		// Token: 0x04000A33 RID: 2611
		OriginatorReturnAddress = 2359554U,
		// Token: 0x04000A34 RID: 2612
		ParentKey = 2425090U,
		// Token: 0x04000A35 RID: 2613
		Priority = 2490371U,
		// Token: 0x04000A36 RID: 2614
		OriginCheck = 2556162U,
		// Token: 0x04000A37 RID: 2615
		ProofOfSubmissionRequested = 2621451U,
		// Token: 0x04000A38 RID: 2616
		ReadReceiptRequested = 2686987U,
		// Token: 0x04000A39 RID: 2617
		ReceiptTime = 2752576U,
		// Token: 0x04000A3A RID: 2618
		RecipientReassignmentProhibited = 2818059U,
		// Token: 0x04000A3B RID: 2619
		RedirectionHistory = 2883842U,
		// Token: 0x04000A3C RID: 2620
		RelatedIpms = 2949378U,
		// Token: 0x04000A3D RID: 2621
		OriginalSensitivity = 3014659U,
		// Token: 0x04000A3E RID: 2622
		Languages = 3080223U,
		// Token: 0x04000A3F RID: 2623
		ReplyTime = 3145792U,
		// Token: 0x04000A40 RID: 2624
		ReportTag = 3211522U,
		// Token: 0x04000A41 RID: 2625
		ReportTime = 3276864U,
		// Token: 0x04000A42 RID: 2626
		ReturnedIpm = 3342347U,
		// Token: 0x04000A43 RID: 2627
		Security = 3407875U,
		// Token: 0x04000A44 RID: 2628
		IncompleteCopy = 3473419U,
		// Token: 0x04000A45 RID: 2629
		Sensitivity = 3538947U,
		// Token: 0x04000A46 RID: 2630
		Subject = 3604511U,
		// Token: 0x04000A47 RID: 2631
		SubjectIpm = 3670274U,
		// Token: 0x04000A48 RID: 2632
		ClientSubmitTime = 3735616U,
		// Token: 0x04000A49 RID: 2633
		ReportName = 3801119U,
		// Token: 0x04000A4A RID: 2634
		SentRepresentingSearchKey = 3866882U,
		// Token: 0x04000A4B RID: 2635
		X400ContentType = 3932418U,
		// Token: 0x04000A4C RID: 2636
		SubjectPrefix = 3997727U,
		// Token: 0x04000A4D RID: 2637
		NonReceiptReason = 4063235U,
		// Token: 0x04000A4E RID: 2638
		ReceivedByEntryId = 4129026U,
		// Token: 0x04000A4F RID: 2639
		ReceivedByName = 4194335U,
		// Token: 0x04000A50 RID: 2640
		SentRepresentingEntryId = 4260098U,
		// Token: 0x04000A51 RID: 2641
		SentRepresentingName = 4325407U,
		// Token: 0x04000A52 RID: 2642
		RcvdRepresentingEntryId = 4391170U,
		// Token: 0x04000A53 RID: 2643
		RcvdRepresentingName = 4456479U,
		// Token: 0x04000A54 RID: 2644
		ReportEntryId = 4522242U,
		// Token: 0x04000A55 RID: 2645
		ReadReceiptEntryId = 4587778U,
		// Token: 0x04000A56 RID: 2646
		MessageSubmissionId = 4653314U,
		// Token: 0x04000A57 RID: 2647
		ProviderSubmitTime = 4718656U,
		// Token: 0x04000A58 RID: 2648
		OriginalSubject = 4784159U,
		// Token: 0x04000A59 RID: 2649
		DiscVal = 4849675U,
		// Token: 0x04000A5A RID: 2650
		OrigMessageClass = 4915231U,
		// Token: 0x04000A5B RID: 2651
		OriginalAuthorEntryId = 4980994U,
		// Token: 0x04000A5C RID: 2652
		OriginalAuthorName = 5046303U,
		// Token: 0x04000A5D RID: 2653
		OriginalSubmitTime = 5111872U,
		// Token: 0x04000A5E RID: 2654
		ReplyRecipientEntries = 5177602U,
		// Token: 0x04000A5F RID: 2655
		ReplyRecipientNames = 5242911U,
		// Token: 0x04000A60 RID: 2656
		ReceivedBySearchKey = 5308674U,
		// Token: 0x04000A61 RID: 2657
		RcvdRepresentingSearchKey = 5374210U,
		// Token: 0x04000A62 RID: 2658
		ReadReceiptSearchKey = 5439746U,
		// Token: 0x04000A63 RID: 2659
		ReportSearchKey = 5505282U,
		// Token: 0x04000A64 RID: 2660
		OriginalDeliveryTime = 5570624U,
		// Token: 0x04000A65 RID: 2661
		OriginalAuthorSearchKey = 5636354U,
		// Token: 0x04000A66 RID: 2662
		MessageToMe = 5701643U,
		// Token: 0x04000A67 RID: 2663
		MessageCcMe = 5767179U,
		// Token: 0x04000A68 RID: 2664
		MessageRecipMe = 5832715U,
		// Token: 0x04000A69 RID: 2665
		OriginalSenderName = 5898271U,
		// Token: 0x04000A6A RID: 2666
		OriginalSenderEntryId = 5964034U,
		// Token: 0x04000A6B RID: 2667
		OriginalSenderSearchKey = 6029570U,
		// Token: 0x04000A6C RID: 2668
		OriginalSentRepresentingName = 6094879U,
		// Token: 0x04000A6D RID: 2669
		OriginalSentRepresentingEntryId = 6160642U,
		// Token: 0x04000A6E RID: 2670
		OriginalSentRepresentingSearchKey = 6226178U,
		// Token: 0x04000A6F RID: 2671
		StartDate = 6291520U,
		// Token: 0x04000A70 RID: 2672
		EndDate = 6357056U,
		// Token: 0x04000A71 RID: 2673
		OwnerApptId = 6422531U,
		// Token: 0x04000A72 RID: 2674
		ResponseRequested = 6488075U,
		// Token: 0x04000A73 RID: 2675
		SentRepresentingAddrType = 6553631U,
		// Token: 0x04000A74 RID: 2676
		SentRepresentingEmailAddress = 6619167U,
		// Token: 0x04000A75 RID: 2677
		SentRepresentingSmtpAddress = 1560412191U,
		// Token: 0x04000A76 RID: 2678
		OriginalSenderAddrType = 6684703U,
		// Token: 0x04000A77 RID: 2679
		OriginalSenderEmailAddress = 6750239U,
		// Token: 0x04000A78 RID: 2680
		OriginalSentRepresentingAddrType = 6815775U,
		// Token: 0x04000A79 RID: 2681
		OriginalSentRepresentingEmailAddress = 6881311U,
		// Token: 0x04000A7A RID: 2682
		ConversationTopic = 7340063U,
		// Token: 0x04000A7B RID: 2683
		ConversationIndex = 7405826U,
		// Token: 0x04000A7C RID: 2684
		OriginalDisplayBcc = 7471135U,
		// Token: 0x04000A7D RID: 2685
		OriginalDisplayCc = 7536671U,
		// Token: 0x04000A7E RID: 2686
		OriginalDisplayTo = 7602207U,
		// Token: 0x04000A7F RID: 2687
		ReceivedByAddrType = 7667743U,
		// Token: 0x04000A80 RID: 2688
		ReceivedByEmailAddress = 7733279U,
		// Token: 0x04000A81 RID: 2689
		ReceivedBySmtpAddress = 1560739871U,
		// Token: 0x04000A82 RID: 2690
		RcvdRepresentingAddrType = 7798815U,
		// Token: 0x04000A83 RID: 2691
		RcvdRepresentingEmailAddress = 7864351U,
		// Token: 0x04000A84 RID: 2692
		RcvdRepresentingSmtpAddress = 1560805407U,
		// Token: 0x04000A85 RID: 2693
		OriginalAuthorAddrType = 7929887U,
		// Token: 0x04000A86 RID: 2694
		OriginalAuthorEmailAddress = 7995423U,
		// Token: 0x04000A87 RID: 2695
		OriginallyIntendedRecipAddrType = 8060959U,
		// Token: 0x04000A88 RID: 2696
		OriginallyIntendedRecipEmailAddress = 8126495U,
		// Token: 0x04000A89 RID: 2697
		TransportMessageHeaders = 8192031U,
		// Token: 0x04000A8A RID: 2698
		Delegation = 8257794U,
		// Token: 0x04000A8B RID: 2699
		TnefCorrelationKey = 8323330U,
		// Token: 0x04000A8C RID: 2700
		RichContent = 8781826U,
		// Token: 0x04000A8D RID: 2701
		Body = 268435487U,
		// Token: 0x04000A8E RID: 2702
		ReportText = 268501023U,
		// Token: 0x04000A8F RID: 2703
		OriginatorAndDlExpansionHistory = 268566786U,
		// Token: 0x04000A90 RID: 2704
		ReportingDlName = 268632322U,
		// Token: 0x04000A91 RID: 2705
		ReportingMtaCertificate = 268697858U,
		// Token: 0x04000A92 RID: 2706
		RtfSyncBodyCrc = 268828675U,
		// Token: 0x04000A93 RID: 2707
		RtfSyncBodyCount = 268894211U,
		// Token: 0x04000A94 RID: 2708
		RtfSyncBodyTag = 268959775U,
		// Token: 0x04000A95 RID: 2709
		RtfCompressed = 269025538U,
		// Token: 0x04000A96 RID: 2710
		AlternateBody = 269091074U,
		// Token: 0x04000A97 RID: 2711
		RtfSyncPrefixCount = 269484035U,
		// Token: 0x04000A98 RID: 2712
		RtfSyncTrailingCount = 269549571U,
		// Token: 0x04000A99 RID: 2713
		OriginallyIntendedRecipEntryId = 269615362U,
		// Token: 0x04000A9A RID: 2714
		BodyHtml = 269680671U,
		// Token: 0x04000A9B RID: 2715
		Html = 269680898U,
		// Token: 0x04000A9C RID: 2716
		NativeBodyInfo = 269877251U,
		// Token: 0x04000A9D RID: 2717
		AnnotationToken = 269943042U,
		// Token: 0x04000A9E RID: 2718
		ItemTemporaryFlag = 278331395U,
		// Token: 0x04000A9F RID: 2719
		DavSubmitData = 281411615U,
		// Token: 0x04000AA0 RID: 2720
		InferredImportanceInternal = 302055429U,
		// Token: 0x04000AA1 RID: 2721
		InferredImportanceOverride = 302120963U,
		// Token: 0x04000AA2 RID: 2722
		InferredUnimportanceInternal = 302186501U,
		// Token: 0x04000AA3 RID: 2723
		PredictedActions = 302256130U,
		// Token: 0x04000AA4 RID: 2724
		GroupingActions = 302321666U,
		// Token: 0x04000AA5 RID: 2725
		PredictedActionsSummary = 302383107U,
		// Token: 0x04000AA6 RID: 2726
		PredictedActionsThresholds = 302448898U,
		// Token: 0x04000AA7 RID: 2727
		IsClutter = 302448651U,
		// Token: 0x04000AA8 RID: 2728
		InferencePredictedReplyForwardReasons = 302514434U,
		// Token: 0x04000AA9 RID: 2729
		InferencePredictedDeleteReasons = 302579970U,
		// Token: 0x04000AAA RID: 2730
		InferencePredictedIgnoreReasons = 302645506U,
		// Token: 0x04000AAB RID: 2731
		OriginalDeliveryFolderInfo = 302711042U,
		// Token: 0x04000AAC RID: 2732
		ContentIntegrityCheck = 201326850U,
		// Token: 0x04000AAD RID: 2733
		ExplicitConversion = 201392131U,
		// Token: 0x04000AAE RID: 2734
		IpmReturnRequested = 201457675U,
		// Token: 0x04000AAF RID: 2735
		MessageToken = 201523458U,
		// Token: 0x04000AB0 RID: 2736
		NdrReasonCode = 201588739U,
		// Token: 0x04000AB1 RID: 2737
		NdrDiagCode = 201654275U,
		// Token: 0x04000AB2 RID: 2738
		NonReceiptNotificationRequested = 201719819U,
		// Token: 0x04000AB3 RID: 2739
		DeliveryPoint = 201785347U,
		// Token: 0x04000AB4 RID: 2740
		OriginatorNonDeliveryReportRequested = 201850891U,
		// Token: 0x04000AB5 RID: 2741
		OriginatorRequestedAlternateRecipient = 201916674U,
		// Token: 0x04000AB6 RID: 2742
		PhysicalDeliveryBureauFaxDelivery = 201981963U,
		// Token: 0x04000AB7 RID: 2743
		PhysicalDeliveryMode = 202047491U,
		// Token: 0x04000AB8 RID: 2744
		PhysicalDeliveryReportRequest = 202113027U,
		// Token: 0x04000AB9 RID: 2745
		PhysicalForwardingAddress = 202178818U,
		// Token: 0x04000ABA RID: 2746
		PhysicalForwardingAddressRequested = 202244107U,
		// Token: 0x04000ABB RID: 2747
		PhysicalForwardingProhibited = 202309643U,
		// Token: 0x04000ABC RID: 2748
		PhysicalRenditionAttributes = 202375426U,
		// Token: 0x04000ABD RID: 2749
		ProofOfDelivery = 202440962U,
		// Token: 0x04000ABE RID: 2750
		ProofOfDeliveryRequested = 202506251U,
		// Token: 0x04000ABF RID: 2751
		RecipientCertificate = 202572034U,
		// Token: 0x04000AC0 RID: 2752
		RecipientNumberForAdvice = 202637343U,
		// Token: 0x04000AC1 RID: 2753
		RecipientType = 202702851U,
		// Token: 0x04000AC2 RID: 2754
		RecipientTrackStatus = 1610547203U,
		// Token: 0x04000AC3 RID: 2755
		RecipientTrackStatusTime = 1610285120U,
		// Token: 0x04000AC4 RID: 2756
		RegisteredMailType = 202768387U,
		// Token: 0x04000AC5 RID: 2757
		ReplyRequested = 202833931U,
		// Token: 0x04000AC6 RID: 2758
		RequestedDeliveryMethod = 202899459U,
		// Token: 0x04000AC7 RID: 2759
		SenderEntryId = 202965250U,
		// Token: 0x04000AC8 RID: 2760
		SenderName = 203030559U,
		// Token: 0x04000AC9 RID: 2761
		SupplementaryInfo = 203096095U,
		// Token: 0x04000ACA RID: 2762
		TypeOfMtsUser = 203161603U,
		// Token: 0x04000ACB RID: 2763
		SenderSearchKey = 203227394U,
		// Token: 0x04000ACC RID: 2764
		SenderAddrType = 203292703U,
		// Token: 0x04000ACD RID: 2765
		SenderEmailAddress = 203358239U,
		// Token: 0x04000ACE RID: 2766
		SenderSmtpAddress = 1560346655U,
		// Token: 0x04000ACF RID: 2767
		INetMailOverrideFormat = 1493303299U,
		// Token: 0x04000AD0 RID: 2768
		INetMailOverrideCharset = 1493368863U,
		// Token: 0x04000AD1 RID: 2769
		ParticipantSID = 203686146U,
		// Token: 0x04000AD2 RID: 2770
		ParticipantGuid = 203751682U,
		// Token: 0x04000AD3 RID: 2771
		ToGroupExpansionRecipients = 203816991U,
		// Token: 0x04000AD4 RID: 2772
		CcGroupExpansionRecipients = 203882527U,
		// Token: 0x04000AD5 RID: 2773
		BccGroupExpansionRecipients = 203948063U,
		// Token: 0x04000AD6 RID: 2774
		CurrentVersion = 234881044U,
		// Token: 0x04000AD7 RID: 2775
		DeleteAfterSubmit = 234946571U,
		// Token: 0x04000AD8 RID: 2776
		DisplayBcc = 235012127U,
		// Token: 0x04000AD9 RID: 2777
		DisplayCc = 235077663U,
		// Token: 0x04000ADA RID: 2778
		DisplayTo = 235143199U,
		// Token: 0x04000ADB RID: 2779
		ParentDisplay = 235208735U,
		// Token: 0x04000ADC RID: 2780
		MessageDeliveryTime = 235274304U,
		// Token: 0x04000ADD RID: 2781
		MessageFlags = 235339779U,
		// Token: 0x04000ADE RID: 2782
		MessageSize = 235405315U,
		// Token: 0x04000ADF RID: 2783
		ParentEntryId = 235471106U,
		// Token: 0x04000AE0 RID: 2784
		SentMailEntryId = 235536642U,
		// Token: 0x04000AE1 RID: 2785
		Correlate = 235667467U,
		// Token: 0x04000AE2 RID: 2786
		CorrelateMtsid = 235733250U,
		// Token: 0x04000AE3 RID: 2787
		DiscreteValues = 235798539U,
		// Token: 0x04000AE4 RID: 2788
		Responsibility = 235864075U,
		// Token: 0x04000AE5 RID: 2789
		SpoolerStatus = 235929603U,
		// Token: 0x04000AE6 RID: 2790
		TransportStatus = 235995139U,
		// Token: 0x04000AE7 RID: 2791
		MessageRecipients = 236060685U,
		// Token: 0x04000AE8 RID: 2792
		MessageAttachments = 236126221U,
		// Token: 0x04000AE9 RID: 2793
		SubmitFlags = 236191747U,
		// Token: 0x04000AEA RID: 2794
		RecipientStatus = 236257283U,
		// Token: 0x04000AEB RID: 2795
		TransportKey = 236322819U,
		// Token: 0x04000AEC RID: 2796
		MsgStatus = 236388355U,
		// Token: 0x04000AED RID: 2797
		MessageDownloadTime = 236453891U,
		// Token: 0x04000AEE RID: 2798
		CreationVersion = 236519444U,
		// Token: 0x04000AEF RID: 2799
		ModifyVersion = 236584980U,
		// Token: 0x04000AF0 RID: 2800
		Hasattach = 236650507U,
		// Token: 0x04000AF1 RID: 2801
		BodyCrc = 236716035U,
		// Token: 0x04000AF2 RID: 2802
		NormalizedSubject = 236781599U,
		// Token: 0x04000AF3 RID: 2803
		RtfInSync = 236912651U,
		// Token: 0x04000AF4 RID: 2804
		AttachSize = 236978179U,
		// Token: 0x04000AF5 RID: 2805
		AttachNum = 237043715U,
		// Token: 0x04000AF6 RID: 2806
		Preprocess = 237109259U,
		// Token: 0x04000AF7 RID: 2807
		InternetArticleNumber = 237174787U,
		// Token: 0x04000AF8 RID: 2808
		OriginatingMtaCertificate = 237306114U,
		// Token: 0x04000AF9 RID: 2809
		ProofOfSubmission = 237371650U,
		// Token: 0x04000AFA RID: 2810
		MessageDeepAttachments = 238682125U,
		// Token: 0x04000AFB RID: 2811
		Read = 241762315U,
		// Token: 0x04000AFC RID: 2812
		AntiVirusVendor = 243597343U,
		// Token: 0x04000AFD RID: 2813
		AntiVirusVersion = 243662851U,
		// Token: 0x04000AFE RID: 2814
		AntiVirusScanStatus = 243728387U,
		// Token: 0x04000AFF RID: 2815
		AntiVirusScanInfo = 243793951U,
		// Token: 0x04000B00 RID: 2816
		TransportAntiVirusStamp = 244715551U,
		// Token: 0x04000B01 RID: 2817
		MessageDatabasePage = 243007491U,
		// Token: 0x04000B02 RID: 2818
		MessageHeaderDatabasePage = 243073027U,
		// Token: 0x04000B03 RID: 2819
		SenderSID = 239927554U,
		// Token: 0x04000B04 RID: 2820
		SenderGuid = 239075586U,
		// Token: 0x04000B05 RID: 2821
		RenewTime = 251723840U,
		// Token: 0x04000B06 RID: 2822
		DeliveryOrRenewTime = 251789376U,
		// Token: 0x04000B07 RID: 2823
		ConversationFamilyId = 251855106U,
		// Token: 0x04000B08 RID: 2824
		LikeCount = 251920387U,
		// Token: 0x04000B09 RID: 2825
		RichContentDeprecated = 251985922U,
		// Token: 0x04000B0A RID: 2826
		PeopleCentricConversationId = 252051459U,
		// Token: 0x04000B0B RID: 2827
		FavoriteDisplayAlias = 2080440350U,
		// Token: 0x04000B0C RID: 2828
		FavoriteDisplayName = 2080374814U,
		// Token: 0x04000B0D RID: 2829
		FavPublicSourceKey = 2080506114U,
		// Token: 0x04000B0E RID: 2830
		FavLevelMask = 2097348611U,
		// Token: 0x04000B0F RID: 2831
		EntryId = 268370178U,
		// Token: 0x04000B10 RID: 2832
		ObjectType = 268304387U,
		// Token: 0x04000B11 RID: 2833
		Icon = 268239106U,
		// Token: 0x04000B12 RID: 2834
		MiniIcon = 268173570U,
		// Token: 0x04000B13 RID: 2835
		StoreEntryid = 268108034U,
		// Token: 0x04000B14 RID: 2836
		StoreRecordKey = 268042498U,
		// Token: 0x04000B15 RID: 2837
		RecordKey = 267976962U,
		// Token: 0x04000B16 RID: 2838
		MappingSignature = 267911426U,
		// Token: 0x04000B17 RID: 2839
		AccessLevel = 267845635U,
		// Token: 0x04000B18 RID: 2840
		InstanceKey = 267780354U,
		// Token: 0x04000B19 RID: 2841
		RowType = 267714563U,
		// Token: 0x04000B1A RID: 2842
		Access = 267649027U,
		// Token: 0x04000B1B RID: 2843
		MessageAnnotation = 252575775U,
		// Token: 0x04000B1C RID: 2844
		RowId = 805306371U,
		// Token: 0x04000B1D RID: 2845
		DisplayName = 805371935U,
		// Token: 0x04000B1E RID: 2846
		DisplayNameAnsi = 805371934U,
		// Token: 0x04000B1F RID: 2847
		DisplayNameUnicode,
		// Token: 0x04000B20 RID: 2848
		DisplayNamePrintable = 973013023U,
		// Token: 0x04000B21 RID: 2849
		AddrType = 805437471U,
		// Token: 0x04000B22 RID: 2850
		AddrTypeAnsi = 805437470U,
		// Token: 0x04000B23 RID: 2851
		AddrTypeUnicode,
		// Token: 0x04000B24 RID: 2852
		EmailAddress = 805503007U,
		// Token: 0x04000B25 RID: 2853
		EmailAddressAnsi = 805503006U,
		// Token: 0x04000B26 RID: 2854
		EmailAddressUnicode,
		// Token: 0x04000B27 RID: 2855
		Comment = 805568543U,
		// Token: 0x04000B28 RID: 2856
		Depth = 805634051U,
		// Token: 0x04000B29 RID: 2857
		ProviderDisplay = 805699615U,
		// Token: 0x04000B2A RID: 2858
		CreationTime = 805765184U,
		// Token: 0x04000B2B RID: 2859
		LastModificationTime = 805830720U,
		// Token: 0x04000B2C RID: 2860
		ResourceFlags = 805896195U,
		// Token: 0x04000B2D RID: 2861
		ProviderDllName = 805961759U,
		// Token: 0x04000B2E RID: 2862
		SearchKey = 806027522U,
		// Token: 0x04000B2F RID: 2863
		ProviderUid = 806093058U,
		// Token: 0x04000B30 RID: 2864
		ProviderOrdinal = 806158339U,
		// Token: 0x04000B31 RID: 2865
		TargetEntryId = 806355202U,
		// Token: 0x04000B32 RID: 2866
		ConversationIdObsolete = 806486047U,
		// Token: 0x04000B33 RID: 2867
		ConversationId = 806551810U,
		// Token: 0x04000B34 RID: 2868
		BodyTag = 806617346U,
		// Token: 0x04000B35 RID: 2869
		ConversationIndexTrackingObsolete = 806682644U,
		// Token: 0x04000B36 RID: 2870
		ConversationIndexTracking = 806748171U,
		// Token: 0x04000B37 RID: 2871
		ConversationIdHash = 1747386371U,
		// Token: 0x04000B38 RID: 2872
		ConversationDocumentId = 1747320835U,
		// Token: 0x04000B39 RID: 2873
		InternetMessageIdHash = 1644167171U,
		// Token: 0x04000B3A RID: 2874
		ConversationTopicHash = 1644232707U,
		// Token: 0x04000B3B RID: 2875
		ConversationTopicHashEntries = 912261378U,
		// Token: 0x04000B3C RID: 2876
		ConversationLastMemberDocumentId = 1761607683U,
		// Token: 0x04000B3D RID: 2877
		ConversationPreview = 1761673247U,
		// Token: 0x04000B3E RID: 2878
		ConversationLastMemberDocumentIdMailboxWide = 1761738755U,
		// Token: 0x04000B3F RID: 2879
		ConversationInitialMemberDocumentId = 1761804291U,
		// Token: 0x04000B40 RID: 2880
		ConversationMemberDocumentIds = 1761873923U,
		// Token: 0x04000B41 RID: 2881
		ConversationMessageDeliveryOrRenewTimeMailboxWide = 1761935424U,
		// Token: 0x04000B42 RID: 2882
		FamilyId = 1762001154U,
		// Token: 0x04000B43 RID: 2883
		ConversationMessageRichContentMailboxWide = 1762070530U,
		// Token: 0x04000B44 RID: 2884
		ConversationPreviewMailboxWide = 1762131999U,
		// Token: 0x04000B45 RID: 2885
		ConversationMessageDeliveryOrRenewTime = 1762197568U,
		// Token: 0x04000B46 RID: 2886
		ConversationWorkingSetSourcePartition = 1762263071U,
		// Token: 0x04000B47 RID: 2887
		FormVersion = 855703583U,
		// Token: 0x04000B48 RID: 2888
		FormClsid = 855769160U,
		// Token: 0x04000B49 RID: 2889
		FormContactName = 855834655U,
		// Token: 0x04000B4A RID: 2890
		FormCategory = 855900191U,
		// Token: 0x04000B4B RID: 2891
		FormCategorySub = 855965727U,
		// Token: 0x04000B4C RID: 2892
		FormHostMap = 856035331U,
		// Token: 0x04000B4D RID: 2893
		FormHidden = 856096779U,
		// Token: 0x04000B4E RID: 2894
		FormDesignerName = 856162335U,
		// Token: 0x04000B4F RID: 2895
		FormDesignerGuid = 856227912U,
		// Token: 0x04000B50 RID: 2896
		FormMessageBehavior = 856293379U,
		// Token: 0x04000B51 RID: 2897
		DefaultStore = 872415243U,
		// Token: 0x04000B52 RID: 2898
		StoreSupportMask = 873267203U,
		// Token: 0x04000B53 RID: 2899
		StoreState = 873332739U,
		// Token: 0x04000B54 RID: 2900
		IpmSubtreeSearchKey = 873464066U,
		// Token: 0x04000B55 RID: 2901
		IpmOutboxSearchKey = 873529602U,
		// Token: 0x04000B56 RID: 2902
		IpmWastebasketSearchKey = 873595138U,
		// Token: 0x04000B57 RID: 2903
		IpmSentMailSearchKey = 873660674U,
		// Token: 0x04000B58 RID: 2904
		MdbProvider = 873726210U,
		// Token: 0x04000B59 RID: 2905
		ReceiveFolderSettings = 873791501U,
		// Token: 0x04000B5A RID: 2906
		ValidFolderMask = 903806979U,
		// Token: 0x04000B5B RID: 2907
		IpmSubtreeEntryId = 903872770U,
		// Token: 0x04000B5C RID: 2908
		IpmInboxEntryId = 903938306U,
		// Token: 0x04000B5D RID: 2909
		IpmOutboxEntryId = 904003842U,
		// Token: 0x04000B5E RID: 2910
		IpmSentMailEntryId = 904134914U,
		// Token: 0x04000B5F RID: 2911
		ViewsEntryId = 904200450U,
		// Token: 0x04000B60 RID: 2912
		CommonViewsEntryId = 904265986U,
		// Token: 0x04000B61 RID: 2913
		FinderEntryId = 904331522U,
		// Token: 0x04000B62 RID: 2914
		SpoolerQueueEntryId = 904397058U,
		// Token: 0x04000B63 RID: 2915
		ConversationsFolderEntryId = 904659202U,
		// Token: 0x04000B64 RID: 2916
		RootEntryId = 903348482U,
		// Token: 0x04000B65 RID: 2917
		AllItemsEntryId = 904790274U,
		// Token: 0x04000B66 RID: 2918
		SharingFolderEntryId = 904855810U,
		// Token: 0x04000B67 RID: 2919
		CISearchEnabled = 240910347U,
		// Token: 0x04000B68 RID: 2920
		LocalDirectoryEntryId = 873857282U,
		// Token: 0x04000B69 RID: 2921
		OwnerLogonUserConfigurationCache = 910557442U,
		// Token: 0x04000B6A RID: 2922
		ControlDataForCalendarRepairAssistant = 874512642U,
		// Token: 0x04000B6B RID: 2923
		ControlDataForSharingPolicyAssistant = 874578178U,
		// Token: 0x04000B6C RID: 2924
		ControlDataForElcAssistant = 874643714U,
		// Token: 0x04000B6D RID: 2925
		ControlDataForTopNWordsAssistant = 874709250U,
		// Token: 0x04000B6E RID: 2926
		ControlDataForJunkEmailAssistant = 874774786U,
		// Token: 0x04000B6F RID: 2927
		ControlDataForCalendarSyncAssistant = 874840322U,
		// Token: 0x04000B70 RID: 2928
		ExternalSharingCalendarSubscriptionCount = 874905603U,
		// Token: 0x04000B71 RID: 2929
		ControlDataForUMReportingAssistant = 874971394U,
		// Token: 0x04000B72 RID: 2930
		HasUMReportData = 875036683U,
		// Token: 0x04000B73 RID: 2931
		InternetCalendarSubscriptionCount = 875102211U,
		// Token: 0x04000B74 RID: 2932
		ExternalSharingContactSubscriptionCount = 875167747U,
		// Token: 0x04000B75 RID: 2933
		JunkEmailSafeListDirty = 875233283U,
		// Token: 0x04000B76 RID: 2934
		IsTopNEnabled = 875298827U,
		// Token: 0x04000B77 RID: 2935
		LastSharingPolicyAppliedId = 875364610U,
		// Token: 0x04000B78 RID: 2936
		LastSharingPolicyAppliedHash = 875430146U,
		// Token: 0x04000B79 RID: 2937
		LastSharingPolicyAppliedTime = 875495488U,
		// Token: 0x04000B7A RID: 2938
		OofScheduleStart = 875561024U,
		// Token: 0x04000B7B RID: 2939
		OofScheduleEnd = 875626560U,
		// Token: 0x04000B7C RID: 2940
		ControlDataForDirectoryProcessorAssistant = 875692290U,
		// Token: 0x04000B7D RID: 2941
		NeedsDirectoryProcessor = 875757579U,
		// Token: 0x04000B7E RID: 2942
		RetentionQueryIds = 875827231U,
		// Token: 0x04000B7F RID: 2943
		RetentionQueryInfo = 875888660U,
		// Token: 0x04000B80 RID: 2944
		ControlDataForPublicFolderAssistant = 876019970U,
		// Token: 0x04000B81 RID: 2945
		MailboxMarked = 875888651U,
		// Token: 0x04000B82 RID: 2946
		MailboxLastProcessedTimestamp = 875954240U,
		// Token: 0x04000B83 RID: 2947
		ControlDataForInferenceTrainingAssistant = 876085506U,
		// Token: 0x04000B84 RID: 2948
		InferenceEnabled = 876150795U,
		// Token: 0x04000B85 RID: 2949
		ControlDataForContactLinkingAssistant = 876216578U,
		// Token: 0x04000B86 RID: 2950
		ContactLinking = 876281859U,
		// Token: 0x04000B87 RID: 2951
		ControlDataForOABGeneratorAssistant = 876347650U,
		// Token: 0x04000B88 RID: 2952
		ContactSaveVersion = 876412931U,
		// Token: 0x04000B89 RID: 2953
		ControlDataForOrgContactsSyncAssistant = 876478722U,
		// Token: 0x04000B8A RID: 2954
		OrgContactsSyncTimestamp = 876544064U,
		// Token: 0x04000B8B RID: 2955
		PushNotificationSubscriptionType = 876609794U,
		// Token: 0x04000B8C RID: 2956
		OrgContactsSyncADWatermark = 876675136U,
		// Token: 0x04000B8D RID: 2957
		ControlDataForInferenceDataCollectionAssistant = 876740866U,
		// Token: 0x04000B8E RID: 2958
		InferenceDataCollectionProcessingState = 876806402U,
		// Token: 0x04000B8F RID: 2959
		ControlDataForPeopleRelevanceAssistant = 876871938U,
		// Token: 0x04000B90 RID: 2960
		SiteMailboxInternalState = 876937219U,
		// Token: 0x04000B91 RID: 2961
		ControlDataForSiteMailboxAssistant = 877003010U,
		// Token: 0x04000B92 RID: 2962
		InferenceTrainingLastContentCount = 877068291U,
		// Token: 0x04000B93 RID: 2963
		InferenceTrainingLastAttemptTimestamp = 877133888U,
		// Token: 0x04000B94 RID: 2964
		InferenceTrainingLastSuccessTimestamp = 877199424U,
		// Token: 0x04000B95 RID: 2965
		InferenceUserCapabilityFlags = 877264899U,
		// Token: 0x04000B96 RID: 2966
		ControlDataForMailboxAssociationReplicationAssistant = 877330690U,
		// Token: 0x04000B97 RID: 2967
		MailboxAssociationNextReplicationTime = 877396032U,
		// Token: 0x04000B98 RID: 2968
		MailboxAssociationProcessingFlags = 877461507U,
		// Token: 0x04000B99 RID: 2969
		ControlDataForSharePointSignalStoreAssistant = 877527298U,
		// Token: 0x04000B9A RID: 2970
		ControlDataForPeopleCentricTriageAssistant = 877592834U,
		// Token: 0x04000B9B RID: 2971
		NotificationBrokerSubscriptions = 877658115U,
		// Token: 0x04000B9C RID: 2972
		GroupMailboxPermissionsVersion = 877723651U,
		// Token: 0x04000B9D RID: 2973
		ElcLastRunTotalProcessingTime = 877789204U,
		// Token: 0x04000B9E RID: 2974
		ElcLastRunSubAssistantProcessingTime = 877854740U,
		// Token: 0x04000B9F RID: 2975
		ElcLastRunUpdatedFolderCount = 877920276U,
		// Token: 0x04000BA0 RID: 2976
		ElcLastRunTaggedFolderCount = 877985812U,
		// Token: 0x04000BA1 RID: 2977
		ElcLastRunUpdatedItemCount = 878051348U,
		// Token: 0x04000BA2 RID: 2978
		ElcLastRunTaggedWithArchiveItemCount = 878116884U,
		// Token: 0x04000BA3 RID: 2979
		ElcLastRunTaggedWithExpiryItemCount = 878182420U,
		// Token: 0x04000BA4 RID: 2980
		ElcLastRunDeletedFromRootItemCount = 878247956U,
		// Token: 0x04000BA5 RID: 2981
		ElcLastRunDeletedFromDumpsterItemCount = 878313492U,
		// Token: 0x04000BA6 RID: 2982
		ElcLastRunArchivedFromRootItemCount = 878379028U,
		// Token: 0x04000BA7 RID: 2983
		ElcLastRunArchivedFromDumpsterItemCount = 878444564U,
		// Token: 0x04000BA8 RID: 2984
		ScheduledISIntegLastFinished = 878510144U,
		// Token: 0x04000BA9 RID: 2985
		ControlDataForSearchIndexRepairAssistant = 878575874U,
		// Token: 0x04000BAA RID: 2986
		ELCLastSuccessTimestamp = 878641216U,
		// Token: 0x04000BAB RID: 2987
		EventEmailReminderTimer = 878706752U,
		// Token: 0x04000BAC RID: 2988
		InferenceTruthLoggingLastAttemptTimestamp = 878772288U,
		// Token: 0x04000BAD RID: 2989
		InferenceTruthLoggingLastSuccessTimestamp = 878837824U,
		// Token: 0x04000BAE RID: 2990
		ControlDataForGroupMailboxAssistant = 878903554U,
		// Token: 0x04000BAF RID: 2991
		ItemsPendingUpgrade = 878968835U,
		// Token: 0x04000BB0 RID: 2992
		ConsumerSharingCalendarSubscriptionCount = 879034371U,
		// Token: 0x04000BB1 RID: 2993
		GroupMailboxGeneratedPhotoVersion = 879099907U,
		// Token: 0x04000BB2 RID: 2994
		GroupMailboxGeneratedPhotoSignature = 879165698U,
		// Token: 0x04000BB3 RID: 2995
		GroupMailboxExchangeResourcesPublishedVersion = 879230979U,
		// Token: 0x04000BB4 RID: 2996
		UnsearchableItemsStream = 905838850U,
		// Token: 0x04000BB5 RID: 2997
		ContainerFlags = 905969667U,
		// Token: 0x04000BB6 RID: 2998
		FolderType = 906035203U,
		// Token: 0x04000BB7 RID: 2999
		ContentCount = 906100739U,
		// Token: 0x04000BB8 RID: 3000
		ContentUnread = 906166275U,
		// Token: 0x04000BB9 RID: 3001
		CreateTemplates = 906231821U,
		// Token: 0x04000BBA RID: 3002
		DetailsTable = 906297357U,
		// Token: 0x04000BBB RID: 3003
		Search = 906428429U,
		// Token: 0x04000BBC RID: 3004
		Selectable = 906559499U,
		// Token: 0x04000BBD RID: 3005
		SubFolders = 906625035U,
		// Token: 0x04000BBE RID: 3006
		Status = 906690563U,
		// Token: 0x04000BBF RID: 3007
		Anr = 906756127U,
		// Token: 0x04000BC0 RID: 3008
		ContentsSortOrder = 906825731U,
		// Token: 0x04000BC1 RID: 3009
		ContainerHierarchy = 906887181U,
		// Token: 0x04000BC2 RID: 3010
		ContainerContents = 906952717U,
		// Token: 0x04000BC3 RID: 3011
		FolderAssociatedContents = 907018253U,
		// Token: 0x04000BC4 RID: 3012
		DefCreateDl = 907084034U,
		// Token: 0x04000BC5 RID: 3013
		DefCreateMailuser = 907149570U,
		// Token: 0x04000BC6 RID: 3014
		ContainerClass = 907214879U,
		// Token: 0x04000BC7 RID: 3015
		ContainerModifyVersion = 907280404U,
		// Token: 0x04000BC8 RID: 3016
		AbProviderId = 907346178U,
		// Token: 0x04000BC9 RID: 3017
		DefaultViewEntryId = 907411714U,
		// Token: 0x04000BCA RID: 3018
		AssocContentCount = 907476995U,
		// Token: 0x04000BCB RID: 3019
		SearchFolderMsgCount = 910426115U,
		// Token: 0x04000BCC RID: 3020
		AllowAgeout = 908001291U,
		// Token: 0x04000BCD RID: 3021
		SearchFolderAgeOutTimeout = 910622723U,
		// Token: 0x04000BCE RID: 3022
		SearchFolderPopulationResult = 910688259U,
		// Token: 0x04000BCF RID: 3023
		SearchFolderPopulationDiagnostics = 910754050U,
		// Token: 0x04000BD0 RID: 3024
		ContentAggregationFlags = 915341315U,
		// Token: 0x04000BD1 RID: 3025
		LinkedSiteAuthorityUrl = 1715929119U,
		// Token: 0x04000BD2 RID: 3026
		PromotedProperties = 1715798274U,
		// Token: 0x04000BD3 RID: 3027
		Fid = 1732771860U,
		// Token: 0x04000BD4 RID: 3028
		SearchBacklinkNames = 1736904735U,
		// Token: 0x04000BD5 RID: 3029
		MidsetDeletedExport = 1734082818U,
		// Token: 0x04000BD6 RID: 3030
		AttachmentX400Parameters = 922747138U,
		// Token: 0x04000BD7 RID: 3031
		AttachDataObj = 922812429U,
		// Token: 0x04000BD8 RID: 3032
		AttachDataBin = 922812674U,
		// Token: 0x04000BD9 RID: 3033
		AttachEncoding = 922878210U,
		// Token: 0x04000BDA RID: 3034
		AttachExtension = 922943519U,
		// Token: 0x04000BDB RID: 3035
		AttachFileName = 923009055U,
		// Token: 0x04000BDC RID: 3036
		AttachMethod = 923074563U,
		// Token: 0x04000BDD RID: 3037
		AttachLongFileName = 923205663U,
		// Token: 0x04000BDE RID: 3038
		AttachPathName = 923271199U,
		// Token: 0x04000BDF RID: 3039
		AttachRendering = 923336962U,
		// Token: 0x04000BE0 RID: 3040
		AttachTag = 923402498U,
		// Token: 0x04000BE1 RID: 3041
		RenderingPosition = 923467779U,
		// Token: 0x04000BE2 RID: 3042
		AttachTransportName = 923533343U,
		// Token: 0x04000BE3 RID: 3043
		AttachLongPathName = 923598879U,
		// Token: 0x04000BE4 RID: 3044
		AttachMimeTag = 923664415U,
		// Token: 0x04000BE5 RID: 3045
		AttachAdditionalInfo = 923730178U,
		// Token: 0x04000BE6 RID: 3046
		AttachContentLocation = 923992095U,
		// Token: 0x04000BE7 RID: 3047
		AttachFlags = 924057603U,
		// Token: 0x04000BE8 RID: 3048
		AttachDisposition = 924188703U,
		// Token: 0x04000BE9 RID: 3049
		DisplayType = 956301315U,
		// Token: 0x04000BEA RID: 3050
		TemplateId = 956432642U,
		// Token: 0x04000BEB RID: 3051
		DisplayTypeEx = 956628995U,
		// Token: 0x04000BEC RID: 3052
		TemplateInfoTemplate = 2148991234U,
		// Token: 0x04000BED RID: 3053
		TemplateInfoScript = 2149056770U,
		// Token: 0x04000BEE RID: 3054
		TemplateInfoEmailType = 2152202270U,
		// Token: 0x04000BEF RID: 3055
		TemplateInfoHelpFileName = 2151350302U,
		// Token: 0x04000BF0 RID: 3056
		TemplateInfoHelpFileContents = 2148532482U,
		// Token: 0x04000BF1 RID: 3057
		SearchResultKind = 246415363U,
		// Token: 0x04000BF2 RID: 3058
		SearchAttachments = 1718419487U,
		// Token: 0x04000BF3 RID: 3059
		SearchFullText = 246087711U,
		// Token: 0x04000BF4 RID: 3060
		SearchFullTextSubject = 246153247U,
		// Token: 0x04000BF5 RID: 3061
		SearchFullTextBody = 246218783U,
		// Token: 0x04000BF6 RID: 3062
		SearchFullTextConversationIndex = 246284319U,
		// Token: 0x04000BF7 RID: 3063
		SearchAllIndexedProps = 246349855U,
		// Token: 0x04000BF8 RID: 3064
		SearchRecipients = 246480927U,
		// Token: 0x04000BF9 RID: 3065
		SearchRecipientsTo = 246546463U,
		// Token: 0x04000BFA RID: 3066
		SearchRecipientsCc = 246611999U,
		// Token: 0x04000BFB RID: 3067
		SearchRecipientsBcc = 246677535U,
		// Token: 0x04000BFC RID: 3068
		SearchAccountTo = 246743071U,
		// Token: 0x04000BFD RID: 3069
		SearchAccountCc = 246808607U,
		// Token: 0x04000BFE RID: 3070
		SearchAccountBcc = 246874143U,
		// Token: 0x04000BFF RID: 3071
		SearchEmailAddressTo = 246939679U,
		// Token: 0x04000C00 RID: 3072
		SearchEmailAddressCc = 247005215U,
		// Token: 0x04000C01 RID: 3073
		SearchEmailAddressBcc = 247070751U,
		// Token: 0x04000C02 RID: 3074
		SearchSmtpAddressTo = 247136287U,
		// Token: 0x04000C03 RID: 3075
		SearchSmtpAddressCc = 247201823U,
		// Token: 0x04000C04 RID: 3076
		SearchSmtpAddressBcc = 247267359U,
		// Token: 0x04000C05 RID: 3077
		SearchSender = 247332895U,
		// Token: 0x04000C06 RID: 3078
		SendYearHigh = 247398431U,
		// Token: 0x04000C07 RID: 3079
		SendYearLow = 247463967U,
		// Token: 0x04000C08 RID: 3080
		SendMonth = 247529503U,
		// Token: 0x04000C09 RID: 3081
		SendDayHigh = 247595039U,
		// Token: 0x04000C0A RID: 3082
		SendDayLow = 247660575U,
		// Token: 0x04000C0B RID: 3083
		SendQuarterHigh = 247726111U,
		// Token: 0x04000C0C RID: 3084
		SendQuarterLow = 247791647U,
		// Token: 0x04000C0D RID: 3085
		RcvdYearHigh = 247857183U,
		// Token: 0x04000C0E RID: 3086
		RcvdYearLow = 247922719U,
		// Token: 0x04000C0F RID: 3087
		RcvdMonth = 247988255U,
		// Token: 0x04000C10 RID: 3088
		RcvdDayHigh = 248053791U,
		// Token: 0x04000C11 RID: 3089
		RcvdDayLow = 248119327U,
		// Token: 0x04000C12 RID: 3090
		RcvdQuarterHigh = 248184863U,
		// Token: 0x04000C13 RID: 3091
		RcvdQuarterLow = 248250399U,
		// Token: 0x04000C14 RID: 3092
		IsIrmMessage = 248315915U,
		// Token: 0x04000C15 RID: 3093
		Account = 973078559U,
		// Token: 0x04000C16 RID: 3094
		AlternateRecipient = 973144322U,
		// Token: 0x04000C17 RID: 3095
		CallbackTelephoneNumber = 973209631U,
		// Token: 0x04000C18 RID: 3096
		ConversionProhibited = 973275147U,
		// Token: 0x04000C19 RID: 3097
		DiscloseRecipients = 973340683U,
		// Token: 0x04000C1A RID: 3098
		Generation = 973406239U,
		// Token: 0x04000C1B RID: 3099
		GivenName = 973471775U,
		// Token: 0x04000C1C RID: 3100
		GovernmentIdNumber = 973537311U,
		// Token: 0x04000C1D RID: 3101
		BusinessTelephoneNumber = 973602847U,
		// Token: 0x04000C1E RID: 3102
		HomeTelephoneNumber = 973668383U,
		// Token: 0x04000C1F RID: 3103
		Initials = 973733919U,
		// Token: 0x04000C20 RID: 3104
		Keyword = 973799455U,
		// Token: 0x04000C21 RID: 3105
		Language = 973864991U,
		// Token: 0x04000C22 RID: 3106
		Location = 973930527U,
		// Token: 0x04000C23 RID: 3107
		MailPermission = 973996043U,
		// Token: 0x04000C24 RID: 3108
		MhsCommonName = 974061599U,
		// Token: 0x04000C25 RID: 3109
		OrganizationalIdNumber = 974127135U,
		// Token: 0x04000C26 RID: 3110
		Surname = 974192671U,
		// Token: 0x04000C27 RID: 3111
		OriginalEntryId = 974258434U,
		// Token: 0x04000C28 RID: 3112
		OriginalDisplayName = 974323743U,
		// Token: 0x04000C29 RID: 3113
		OriginalSearchKey = 974389506U,
		// Token: 0x04000C2A RID: 3114
		PostalAddress = 974454815U,
		// Token: 0x04000C2B RID: 3115
		CompanyName = 974520351U,
		// Token: 0x04000C2C RID: 3116
		Title = 974585887U,
		// Token: 0x04000C2D RID: 3117
		DepartmentName = 974651423U,
		// Token: 0x04000C2E RID: 3118
		DepartmentNameAnsi = 974651422U,
		// Token: 0x04000C2F RID: 3119
		DepartmentNameUnicode,
		// Token: 0x04000C30 RID: 3120
		OfficeLocation = 974716959U,
		// Token: 0x04000C31 RID: 3121
		OfficeLocationAnsi = 974716958U,
		// Token: 0x04000C32 RID: 3122
		OfficeLocationUnicode,
		// Token: 0x04000C33 RID: 3123
		PrimaryTelephoneNumber = 974782495U,
		// Token: 0x04000C34 RID: 3124
		PrimaryTelephoneNumberAnsi = 974782494U,
		// Token: 0x04000C35 RID: 3125
		PrimaryTelephoneNumberUnicode,
		// Token: 0x04000C36 RID: 3126
		Business2TelephoneNumber = 974848031U,
		// Token: 0x04000C37 RID: 3127
		MobileTelephoneNumber = 974913567U,
		// Token: 0x04000C38 RID: 3128
		RadioTelephoneNumber = 974979103U,
		// Token: 0x04000C39 RID: 3129
		CarTelephoneNumber = 975044639U,
		// Token: 0x04000C3A RID: 3130
		OtherTelephoneNumber = 975110175U,
		// Token: 0x04000C3B RID: 3131
		TransmitableDisplayName = 975175711U,
		// Token: 0x04000C3C RID: 3132
		TransmitableDisplayNameAnsi = 975175710U,
		// Token: 0x04000C3D RID: 3133
		TransmitableDisplayNameUnicode,
		// Token: 0x04000C3E RID: 3134
		PagerTelephoneNumber = 975241247U,
		// Token: 0x04000C3F RID: 3135
		UserCertificate = 975307010U,
		// Token: 0x04000C40 RID: 3136
		PrimaryFaxNumber = 975372319U,
		// Token: 0x04000C41 RID: 3137
		BusinessFaxNumber = 975437855U,
		// Token: 0x04000C42 RID: 3138
		HomeFaxNumber = 975503391U,
		// Token: 0x04000C43 RID: 3139
		Country = 975568927U,
		// Token: 0x04000C44 RID: 3140
		Locality = 975634463U,
		// Token: 0x04000C45 RID: 3141
		StateOrProvince = 975699999U,
		// Token: 0x04000C46 RID: 3142
		StreetAddress = 975765535U,
		// Token: 0x04000C47 RID: 3143
		PostalCode = 975831071U,
		// Token: 0x04000C48 RID: 3144
		PostOfficeBox = 975896607U,
		// Token: 0x04000C49 RID: 3145
		TelexNumber = 975962143U,
		// Token: 0x04000C4A RID: 3146
		IsdnNumber = 976027679U,
		// Token: 0x04000C4B RID: 3147
		AssistantTelephoneNumber = 976093215U,
		// Token: 0x04000C4C RID: 3148
		Home2TelephoneNumber = 976158751U,
		// Token: 0x04000C4D RID: 3149
		Assistant = 976224287U,
		// Token: 0x04000C4E RID: 3150
		SendRichInfo = 977272843U,
		// Token: 0x04000C4F RID: 3151
		WeddingAnniversary = 977338432U,
		// Token: 0x04000C50 RID: 3152
		Birthday = 977403968U,
		// Token: 0x04000C51 RID: 3153
		Hobbies = 977469471U,
		// Token: 0x04000C52 RID: 3154
		MiddleName = 977535007U,
		// Token: 0x04000C53 RID: 3155
		DisplayNamePrefix = 977600543U,
		// Token: 0x04000C54 RID: 3156
		Profession = 977666079U,
		// Token: 0x04000C55 RID: 3157
		PreferredByName = 977731615U,
		// Token: 0x04000C56 RID: 3158
		SpouseName = 977797151U,
		// Token: 0x04000C57 RID: 3159
		ComputerNetworkName = 977862687U,
		// Token: 0x04000C58 RID: 3160
		CustomerId = 977928223U,
		// Token: 0x04000C59 RID: 3161
		TtytddPhoneNumber = 977993759U,
		// Token: 0x04000C5A RID: 3162
		FtpSite = 978059295U,
		// Token: 0x04000C5B RID: 3163
		Gender = 978124802U,
		// Token: 0x04000C5C RID: 3164
		ManagerName = 978190367U,
		// Token: 0x04000C5D RID: 3165
		Nickname = 978255903U,
		// Token: 0x04000C5E RID: 3166
		PersonalHomePage = 978321439U,
		// Token: 0x04000C5F RID: 3167
		BusinessHomePage = 978386975U,
		// Token: 0x04000C60 RID: 3168
		ContactVersion = 978452552U,
		// Token: 0x04000C61 RID: 3169
		ContactEntryIds = 978522370U,
		// Token: 0x04000C62 RID: 3170
		ContactAddrTypes = 978587679U,
		// Token: 0x04000C63 RID: 3171
		ContactDefaultAddressIndex = 978649091U,
		// Token: 0x04000C64 RID: 3172
		ContactEmailAddresses = 978718751U,
		// Token: 0x04000C65 RID: 3173
		CompanyMainPhoneNumber = 978780191U,
		// Token: 0x04000C66 RID: 3174
		ChildrensNames = 978849823U,
		// Token: 0x04000C67 RID: 3175
		HomeAddressCity = 978911263U,
		// Token: 0x04000C68 RID: 3176
		HomeAddressCountry = 978976799U,
		// Token: 0x04000C69 RID: 3177
		HomeAddressPostalCode = 979042335U,
		// Token: 0x04000C6A RID: 3178
		HomeAddressStateOrProvince = 979107871U,
		// Token: 0x04000C6B RID: 3179
		HomeAddressStreet = 979173407U,
		// Token: 0x04000C6C RID: 3180
		HomeAddressPostOfficeBox = 979238943U,
		// Token: 0x04000C6D RID: 3181
		OtherAddressCity = 979304479U,
		// Token: 0x04000C6E RID: 3182
		OtherAddressCountry = 979370015U,
		// Token: 0x04000C6F RID: 3183
		OtherAddressPostalCode = 979435551U,
		// Token: 0x04000C70 RID: 3184
		OtherAddressStateOrProvince = 979501087U,
		// Token: 0x04000C71 RID: 3185
		OtherAddressStreet = 979566623U,
		// Token: 0x04000C72 RID: 3186
		OtherAddressPostOfficeBox = 979632159U,
		// Token: 0x04000C73 RID: 3187
		UserSMimeCertificate = 980422914U,
		// Token: 0x04000C74 RID: 3188
		SendInternetEncoding = 980484099U,
		// Token: 0x04000C75 RID: 3189
		PartnerNetworkId = 980811807U,
		// Token: 0x04000C76 RID: 3190
		PartnerNetworkUserId = 980877343U,
		// Token: 0x04000C77 RID: 3191
		PartnerNetworkThumbnailPhotoUrl = 980942879U,
		// Token: 0x04000C78 RID: 3192
		PartnerNetworkProfilePhotoUrl = 981008415U,
		// Token: 0x04000C79 RID: 3193
		PartnerNetworkContactType = 981073951U,
		// Token: 0x04000C7A RID: 3194
		RelevanceScore = 981139459U,
		// Token: 0x04000C7B RID: 3195
		IsDistributionListContact = 981205003U,
		// Token: 0x04000C7C RID: 3196
		IsPromotedContact = 981270539U,
		// Token: 0x04000C7D RID: 3197
		StoreProviders = 1023410434U,
		// Token: 0x04000C7E RID: 3198
		AbProviders = 1023475970U,
		// Token: 0x04000C7F RID: 3199
		TransportProviders = 1023541506U,
		// Token: 0x04000C80 RID: 3200
		DefaultProfile = 1023672331U,
		// Token: 0x04000C81 RID: 3201
		AbSearchPath = 1023742210U,
		// Token: 0x04000C82 RID: 3202
		AbDefaultDir = 1023803650U,
		// Token: 0x04000C83 RID: 3203
		AbDefaultPab = 1023869186U,
		// Token: 0x04000C84 RID: 3204
		FilteringHooks = 1023934722U,
		// Token: 0x04000C85 RID: 3205
		ServiceName = 1024000031U,
		// Token: 0x04000C86 RID: 3206
		ServiceDllName = 1024065567U,
		// Token: 0x04000C87 RID: 3207
		ServiceUid = 1024196866U,
		// Token: 0x04000C88 RID: 3208
		ServiceExtraUids = 1024262402U,
		// Token: 0x04000C89 RID: 3209
		Services = 1024327938U,
		// Token: 0x04000C8A RID: 3210
		ServiceSupportFiles = 1024397343U,
		// Token: 0x04000C8B RID: 3211
		ServiceDeleteFiles = 1024462879U,
		// Token: 0x04000C8C RID: 3212
		AbSearchPathUpdate = 1024524546U,
		// Token: 0x04000C8D RID: 3213
		ProfileName = 1024589855U,
		// Token: 0x04000C8E RID: 3214
		EformsLocaleId = 1072234499U,
		// Token: 0x04000C8F RID: 3215
		DeferredSendTime = 1072627776U,
		// Token: 0x04000C90 RID: 3216
		MailboxType = 1035730947U,
		// Token: 0x04000C91 RID: 3217
		MailboxTypeDetail = 1034289155U,
		// Token: 0x04000C92 RID: 3218
		RecipientTypeDetail = 1034289172U,
		// Token: 0x04000C93 RID: 3219
		FolderIdsetIn = 1737621762U,
		// Token: 0x04000C94 RID: 3220
		MailboxOwnerName = 1713111071U,
		// Token: 0x04000C95 RID: 3221
		NonIpmSubtreeEntryId = 1713373442U,
		// Token: 0x04000C96 RID: 3222
		EFormsRegistryEntryId = 1713438978U,
		// Token: 0x04000C97 RID: 3223
		FreeBusyEntryId = 1713504514U,
		// Token: 0x04000C98 RID: 3224
		OfflineAddressBookEntryId = 1713570050U,
		// Token: 0x04000C99 RID: 3225
		LocaleEFormsRegistryEntryId = 1713635586U,
		// Token: 0x04000C9A RID: 3226
		LocalSiteFreeBusyEntryId = 1713701122U,
		// Token: 0x04000C9B RID: 3227
		LocalSiteOfflineAddressBookEntryId = 1713766658U,
		// Token: 0x04000C9C RID: 3228
		OofState = 1713176587U,
		// Token: 0x04000C9D RID: 3229
		OofStateEx = 1745879043U,
		// Token: 0x04000C9E RID: 3230
		OofStateUserChangeTime = 1746075712U,
		// Token: 0x04000C9F RID: 3231
		UserOofSettingsItemId = 1746141442U,
		// Token: 0x04000CA0 RID: 3232
		IpmWasteBasketEntryId = 904069378U,
		// Token: 0x04000CA1 RID: 3233
		CalendarFolderEntryId = 919601410U,
		// Token: 0x04000CA2 RID: 3234
		ContactsFolderEntryId = 919666946U,
		// Token: 0x04000CA3 RID: 3235
		JournalFolderEntryId = 919732482U,
		// Token: 0x04000CA4 RID: 3236
		NotesFolderEntryId = 919798018U,
		// Token: 0x04000CA5 RID: 3237
		TasksFolderEntryId = 919863554U,
		// Token: 0x04000CA6 RID: 3238
		DraftsFolderEntryId = 920060162U,
		// Token: 0x04000CA7 RID: 3239
		DefaultFoldersLocaleId = 921698307U,
		// Token: 0x04000CA8 RID: 3240
		ForceUserClientBackoff = 806420738U,
		// Token: 0x04000CA9 RID: 3241
		InTransitStatus = 1712848899U,
		// Token: 0x04000CAA RID: 3242
		MailboxMiscFlags = 1745223683U,
		// Token: 0x04000CAB RID: 3243
		TransferEnabled = 1714028555U,
		// Token: 0x04000CAC RID: 3244
		ImapSubscribeList = 1710624799U,
		// Token: 0x04000CAD RID: 3245
		MapiEntryIdGuid = 1735131394U,
		// Token: 0x04000CAE RID: 3246
		FastTransfer = 1714356237U,
		// Token: 0x04000CAF RID: 3247
		MailboxQuarantined = 1746534411U,
		// Token: 0x04000CB0 RID: 3248
		MailboxQuarantineDescription = 1746599967U,
		// Token: 0x04000CB1 RID: 3249
		MailboxQuarantineLastCrash = 1746665536U,
		// Token: 0x04000CB2 RID: 3250
		MailboxQuarantineEnd = 1746731072U,
		// Token: 0x04000CB3 RID: 3251
		NextLocalId = 1734410498U,
		// Token: 0x04000CB4 RID: 3252
		Localized = 1735196683U,
		// Token: 0x04000CB5 RID: 3253
		PersistableTenantPartitionHint = 1034813698U,
		// Token: 0x04000CB6 RID: 3254
		PreservingMailboxSignature = 1722286091U,
		// Token: 0x04000CB7 RID: 3255
		MRSPreservingMailboxSignature = 1722351627U,
		// Token: 0x04000CB8 RID: 3256
		MailboxMessagesPerFolderCountWarningQuota = 1722482691U,
		// Token: 0x04000CB9 RID: 3257
		MailboxMessagesPerFolderCountReceiveQuota = 1722548227U,
		// Token: 0x04000CBA RID: 3258
		DumpsterMessagesPerFolderCountWarningQuota = 1722613763U,
		// Token: 0x04000CBB RID: 3259
		DumpsterMessagesPerFolderCountReceiveQuota = 1722679299U,
		// Token: 0x04000CBC RID: 3260
		FolderHierarchyChildrenCountWarningQuota = 1722744835U,
		// Token: 0x04000CBD RID: 3261
		FolderHierarchyChildrenCountReceiveQuota = 1722810371U,
		// Token: 0x04000CBE RID: 3262
		FolderHierarchyDepthWarningQuota = 1722875907U,
		// Token: 0x04000CBF RID: 3263
		FolderHierarchyDepthReceiveQuota = 1722941443U,
		// Token: 0x04000CC0 RID: 3264
		FoldersCountWarningQuota = 1723138051U,
		// Token: 0x04000CC1 RID: 3265
		FoldersCountReceiveQuota = 1723203587U,
		// Token: 0x04000CC2 RID: 3266
		NamedPropertiesCountQuota = 1723269123U,
		// Token: 0x04000CC3 RID: 3267
		CorrelationId = 1037107272U,
		// Token: 0x04000CC4 RID: 3268
		InferenceClientActivityFlags = 1748238339U,
		// Token: 0x04000CC5 RID: 3269
		InferenceOWAUserActivityLoggingEnabled = 1748303883U,
		// Token: 0x04000CC6 RID: 3270
		InferenceOLKUserActivityLoggingEnabled = 1748369419U,
		// Token: 0x04000CC7 RID: 3271
		InferenceTrainedModelVersionBreadCrumb = 1752367362U,
		// Token: 0x04000CC8 RID: 3272
		LongTermEntryIdFromTable = 1718616322U,
		// Token: 0x04000CC9 RID: 3273
		LocalCommitTime = 1728643136U,
		// Token: 0x04000CCA RID: 3274
		NTSD = 237437186U,
		// Token: 0x04000CCB RID: 3275
		NTSDAsXML = 241827871U,
		// Token: 0x04000CCC RID: 3276
		AdminNTSD = 1025573122U,
		// Token: 0x04000CCD RID: 3277
		FreeBusyNTSD = 251658498U,
		// Token: 0x04000CCE RID: 3278
		AclTableAndNTSD = 239010050U,
		// Token: 0x04000CCF RID: 3279
		LocalCommitTimeMax = 1728708672U,
		// Token: 0x04000CD0 RID: 3280
		DeletedCountTotal = 1728774147U,
		// Token: 0x04000CD1 RID: 3281
		UrlName = 1728512031U,
		// Token: 0x04000CD2 RID: 3282
		ReplicaList = 1721237762U,
		// Token: 0x04000CD3 RID: 3283
		CurrentIPMWasteBasketContainerEntryId = 919535874U,
		// Token: 0x04000CD4 RID: 3284
		HasRules = 1715077131U,
		// Token: 0x04000CD5 RID: 3285
		HasModerator = 1715404811U,
		// Token: 0x04000CD6 RID: 3286
		PublishInAddressBook = 1072037899U,
		// Token: 0x04000CD7 RID: 3287
		OverallAgeLimit = 1721303043U,
		// Token: 0x04000CD8 RID: 3288
		RetentionAgeLimit = 1724121091U,
		// Token: 0x04000CD9 RID: 3289
		PfQuotaStyle = 1735983107U,
		// Token: 0x04000CDA RID: 3290
		PfStorageQuota = 1736114179U,
		// Token: 0x04000CDB RID: 3291
		PfOverHardQuotaLimit = 1730215939U,
		// Token: 0x04000CDC RID: 3292
		PfMsgSizeLimit = 1730281475U,
		// Token: 0x04000CDD RID: 3293
		ReplicationStyle = 1720713219U,
		// Token: 0x04000CDE RID: 3294
		ReplicationSchedule = 1720779010U,
		// Token: 0x04000CDF RID: 3295
		DisablePeruserRead = 1724186635U,
		// Token: 0x04000CE0 RID: 3296
		PfMsgAgeLimit = 1728118787U,
		// Token: 0x04000CE1 RID: 3297
		PfProxy = 1729954050U,
		// Token: 0x04000CE2 RID: 3298
		AddressBookEntryId = 1715142914U,
		// Token: 0x04000CE3 RID: 3299
		OofHistory = 1071841538U,
		// Token: 0x04000CE4 RID: 3300
		SystemFolderFlags = 1026490371U,
		// Token: 0x04000CE5 RID: 3301
		IPMFolder = 1738211339U,
		// Token: 0x04000CE6 RID: 3302
		Preview = 1071185951U,
		// Token: 0x04000CE7 RID: 3303
		InternetMessageId = 271908895U,
		// Token: 0x04000CE8 RID: 3304
		AutoResponseSuppress = 1071579139U,
		// Token: 0x04000CE9 RID: 3305
		MessageLocaleId = 1072758787U,
		// Token: 0x04000CEA RID: 3306
		LocalDirectory = 1747452162U,
		// Token: 0x04000CEB RID: 3307
		RecipientFlags = 1610416131U,
		// Token: 0x04000CEC RID: 3308
		SmtpAddress = 972947487U,
		// Token: 0x04000CED RID: 3309
		AclTable = 1071644685U,
		// Token: 0x04000CEE RID: 3310
		RulesTable = 1071710221U,
		// Token: 0x04000CEF RID: 3311
		OofReplyType = 1082130435U,
		// Token: 0x04000CF0 RID: 3312
		ElcAutoCopyLabel = 1082195998U,
		// Token: 0x04000CF1 RID: 3313
		ElcAutoCopyTag = 1729495298U,
		// Token: 0x04000CF2 RID: 3314
		ElcMoveDate = 1729560834U,
		// Token: 0x04000CF3 RID: 3315
		MemberId = 1718681620U,
		// Token: 0x04000CF4 RID: 3316
		MemberName = 1718747166U,
		// Token: 0x04000CF5 RID: 3317
		MemberNameW,
		// Token: 0x04000CF6 RID: 3318
		MemberEmail = 1747517471U,
		// Token: 0x04000CF7 RID: 3319
		MemberExternalId = 1747583007U,
		// Token: 0x04000CF8 RID: 3320
		MemberSID = 1747648770U,
		// Token: 0x04000CF9 RID: 3321
		MemberEntryId = 268370178U,
		// Token: 0x04000CFA RID: 3322
		MemberRights = 1718812675U,
		// Token: 0x04000CFB RID: 3323
		MemberSecurityIdentifier = 1718878466U,
		// Token: 0x04000CFC RID: 3324
		MemberIsGroup = 1718943755U,
		// Token: 0x04000CFD RID: 3325
		HierarchySynchronizer = 1714159629U,
		// Token: 0x04000CFE RID: 3326
		ContentsSynchronizer = 1714225165U,
		// Token: 0x04000CFF RID: 3327
		Collector = 1714290701U,
		// Token: 0x04000D00 RID: 3328
		LocalHostName = 1711276063U,
		// Token: 0x04000D01 RID: 3329
		InternetConversionHints = 1711341571U,
		// Token: 0x04000D02 RID: 3330
		InternetConversionStream = 1711407107U,
		// Token: 0x04000D03 RID: 3331
		InternetConversionCallerVersion = 1711472671U,
		// Token: 0x04000D04 RID: 3332
		MaxReceivedHeaders = 1711538179U,
		// Token: 0x04000D05 RID: 3333
		SendRichInfoOnly = 1711865867U,
		// Token: 0x04000D06 RID: 3334
		SendNativeBody = 1714880523U,
		// Token: 0x04000D07 RID: 3335
		InternetTransmitInfo = 1711800323U,
		// Token: 0x04000D08 RID: 3336
		InternetMessageFormat = 1712324611U,
		// Token: 0x04000D09 RID: 3337
		InternetMessageTextFormat = 1712390147U,
		// Token: 0x04000D0A RID: 3338
		InternetWrappingLength = 1712455683U,
		// Token: 0x04000D0B RID: 3339
		InternetConvObjFlags = 1712586755U,
		// Token: 0x04000D0C RID: 3340
		InternetRequestLines = 1712652291U,
		// Token: 0x04000D0D RID: 3341
		InternetHeaderLength = 1712717827U,
		// Token: 0x04000D0E RID: 3342
		InternetAddressingOptions = 1712783363U,
		// Token: 0x04000D0F RID: 3343
		InternetMessageOptions = 1712848899U,
		// Token: 0x04000D10 RID: 3344
		InternetAddressConversion = 1712914435U,
		// Token: 0x04000D11 RID: 3345
		InternetTemporaryFilename = 1712979999U,
		// Token: 0x04000D12 RID: 3346
		InternetRFC821From = 1713045535U,
		// Token: 0x04000D13 RID: 3347
		InternetLastError = 1713111043U,
		// Token: 0x04000D14 RID: 3348
		InternetFont = 1713176579U,
		// Token: 0x04000D15 RID: 3349
		InternetExternalNewsItem = 1701838851U,
		// Token: 0x04000D16 RID: 3350
		InternetSiteAddress = 1713176607U,
		// Token: 0x04000D17 RID: 3351
		InternetRequestHeaders = 1713242115U,
		// Token: 0x04000D18 RID: 3352
		InternetClientHostIPName = 1714487327U,
		// Token: 0x04000D19 RID: 3353
		ConnectTime = 1723859008U,
		// Token: 0x04000D1A RID: 3354
		ConnectFlags = 1723924483U,
		// Token: 0x04000D1B RID: 3355
		LogonCount = 1723990019U,
		// Token: 0x04000D1C RID: 3356
		HostAddress = 1721696287U,
		// Token: 0x04000D1D RID: 3357
		LocaleId = 1721827331U,
		// Token: 0x04000D1E RID: 3358
		CodePageId = 1724055555U,
		// Token: 0x04000D1F RID: 3359
		SortLocaleId = 1728380931U,
		// Token: 0x04000D20 RID: 3360
		MessageSizeExtended = 235405332U,
		// Token: 0x04000D21 RID: 3361
		NTUserName = 1721761823U,
		// Token: 0x04000D22 RID: 3362
		LastLogonTime = 1721892928U,
		// Token: 0x04000D23 RID: 3363
		LastLogoffTime = 1721958464U,
		// Token: 0x04000D24 RID: 3364
		StorageLimitInformation = 1722023939U,
		// Token: 0x04000D25 RID: 3365
		InternetMdns = 1722089483U,
		// Token: 0x04000D26 RID: 3366
		DeletedMessageSizeExtended = 1721434132U,
		// Token: 0x04000D27 RID: 3367
		DeletedNormalMessageSizeExtended = 1721499668U,
		// Token: 0x04000D28 RID: 3368
		DeleteAssocMessageSizeExtended = 1721565204U,
		// Token: 0x04000D29 RID: 3369
		DeletedMsgCount = 1715470339U,
		// Token: 0x04000D2A RID: 3370
		DeletedAssocMsgCount = 1715666947U,
		// Token: 0x04000D2B RID: 3371
		UserGuid = 1728512258U,
		// Token: 0x04000D2C RID: 3372
		DateDiscoveredAbsentInDS = 1728577600U,
		// Token: 0x04000D2D RID: 3373
		AdminNickName = 1735917599U,
		// Token: 0x04000D2E RID: 3374
		QuotaWarningThreshold = 1730215939U,
		// Token: 0x04000D2F RID: 3375
		QuotaSendThreshold = 1730281475U,
		// Token: 0x04000D30 RID: 3376
		QuotaReceiveThreshold = 1730347011U,
		// Token: 0x04000D31 RID: 3377
		LastOpTime = 1725890624U,
		// Token: 0x04000D32 RID: 3378
		ClientVersion = 1728315423U,
		// Token: 0x04000D33 RID: 3379
		PacketRate = 1727987715U,
		// Token: 0x04000D34 RID: 3380
		LogonTime = 1724645440U,
		// Token: 0x04000D35 RID: 3381
		LogonFlags = 1724710915U,
		// Token: 0x04000D36 RID: 3382
		MsgHeaderFid = 1734606868U,
		// Token: 0x04000D37 RID: 3383
		MailboxDisplayName = 1724448799U,
		// Token: 0x04000D38 RID: 3384
		MailboxDN = 1724383263U,
		// Token: 0x04000D39 RID: 3385
		UserDisplayName = 1724579871U,
		// Token: 0x04000D3A RID: 3386
		UserDN = 1724514335U,
		// Token: 0x04000D3B RID: 3387
		ApplicationId = 1728643103U,
		// Token: 0x04000D3C RID: 3388
		SessionId = 1729101844U,
		// Token: 0x04000D3D RID: 3389
		OpenMessageCount = 1724841987U,
		// Token: 0x04000D3E RID: 3390
		OpenFolderCount = 1724907523U,
		// Token: 0x04000D3F RID: 3391
		OpenAttachCount = 1724973059U,
		// Token: 0x04000D40 RID: 3392
		OpenContentCount = 1725038595U,
		// Token: 0x04000D41 RID: 3393
		OpenHierarchyCount = 1725104131U,
		// Token: 0x04000D42 RID: 3394
		OpenNotifyCount = 1725169667U,
		// Token: 0x04000D43 RID: 3395
		OpenAttachTableCount = 1725235203U,
		// Token: 0x04000D44 RID: 3396
		OpenACLTableCount = 1725300739U,
		// Token: 0x04000D45 RID: 3397
		OpenRulesTableCount = 1725366275U,
		// Token: 0x04000D46 RID: 3398
		OpenStreamsCount = 1725431811U,
		// Token: 0x04000D47 RID: 3399
		OpenFXSrcStreamCount = 1725497347U,
		// Token: 0x04000D48 RID: 3400
		OpenFXDestStreamCount = 1725562883U,
		// Token: 0x04000D49 RID: 3401
		OpenContentRegularCount = 1725628419U,
		// Token: 0x04000D4A RID: 3402
		OpenContentCategCount = 1725693955U,
		// Token: 0x04000D4B RID: 3403
		OpenContentRestrictedCount = 1725759491U,
		// Token: 0x04000D4C RID: 3404
		OpenContentCategAndRestrictedCount = 1725825027U,
		// Token: 0x04000D4D RID: 3405
		MessagingOpRate = 1723334659U,
		// Token: 0x04000D4E RID: 3406
		FolderOpRate = 1723400195U,
		// Token: 0x04000D4F RID: 3407
		TableOpRate = 1723465731U,
		// Token: 0x04000D50 RID: 3408
		TransferOpRate = 1723531267U,
		// Token: 0x04000D51 RID: 3409
		StreamOpRate = 1723596803U,
		// Token: 0x04000D52 RID: 3410
		ProgressOpRate = 1723662339U,
		// Token: 0x04000D53 RID: 3411
		OtherOpRate = 1723727875U,
		// Token: 0x04000D54 RID: 3412
		TotalOpRate = 1723793411U,
		// Token: 0x04000D55 RID: 3413
		PfProxyRequired = 1730084875U,
		// Token: 0x04000D56 RID: 3414
		ClientMode = 1730347011U,
		// Token: 0x04000D57 RID: 3415
		ClientIP = 1730412802U,
		// Token: 0x04000D58 RID: 3416
		ClientIPMask = 1730478338U,
		// Token: 0x04000D59 RID: 3417
		ClientMacAddress = 1730543874U,
		// Token: 0x04000D5A RID: 3418
		ClientMachineName = 1730609183U,
		// Token: 0x04000D5B RID: 3419
		ClientAdapterSpeed = 1730674691U,
		// Token: 0x04000D5C RID: 3420
		ClientRpcsAttempted = 1730740227U,
		// Token: 0x04000D5D RID: 3421
		ClientRpcsSucceeded = 1730805763U,
		// Token: 0x04000D5E RID: 3422
		ClientRpcErrors = 1730871554U,
		// Token: 0x04000D5F RID: 3423
		ClientLatency = 1730936835U,
		// Token: 0x04000D60 RID: 3424
		ProhibitReceiveQuota = 1718222851U,
		// Token: 0x04000D61 RID: 3425
		MaxSubmitMessageSize = 1718419459U,
		// Token: 0x04000D62 RID: 3426
		ProhibitSendQuota = 1718484995U,
		// Token: 0x04000D63 RID: 3427
		SubmittedByAdmin = 1718550539U,
		// Token: 0x04000D64 RID: 3428
		ObjectClassFlags = 1745682435U,
		// Token: 0x04000D65 RID: 3429
		MaxMessageSize = 1747779587U,
		// Token: 0x04000D66 RID: 3430
		TimeInServer = 1731002371U,
		// Token: 0x04000D67 RID: 3431
		TimeInCPU = 1731067907U,
		// Token: 0x04000D68 RID: 3432
		ROPCount = 1731133443U,
		// Token: 0x04000D69 RID: 3433
		PageRead = 1731198979U,
		// Token: 0x04000D6A RID: 3434
		PagePreread = 1731264515U,
		// Token: 0x04000D6B RID: 3435
		LogRecordCount = 1731330051U,
		// Token: 0x04000D6C RID: 3436
		LogRecordBytes = 1731395587U,
		// Token: 0x04000D6D RID: 3437
		LdapReads = 1731461123U,
		// Token: 0x04000D6E RID: 3438
		LdapSearches = 1731526659U,
		// Token: 0x04000D6F RID: 3439
		DigestCategory = 1731592223U,
		// Token: 0x04000D70 RID: 3440
		SampleId = 1731657731U,
		// Token: 0x04000D71 RID: 3441
		SampleTime = 1731723328U,
		// Token: 0x04000D72 RID: 3442
		WorkerProcessId = 1721171971U,
		// Token: 0x04000D73 RID: 3443
		MinimumDatabaseSchemaVersion = 1721237507U,
		// Token: 0x04000D74 RID: 3444
		MaximumDatabaseSchemaVersion = 1721303043U,
		// Token: 0x04000D75 RID: 3445
		MailboxDatabaseVersion = 1721368579U,
		// Token: 0x04000D76 RID: 3446
		CurrentDatabaseSchemaVersion = 1721368579U,
		// Token: 0x04000D77 RID: 3447
		RequestedDatabaseSchemaVersion = 1721434115U,
		// Token: 0x04000D78 RID: 3448
		ReplyTemplateID = 1707213058U,
		// Token: 0x04000D79 RID: 3449
		DeferredActionFolderEntryID = 1713307906U,
		// Token: 0x04000D7A RID: 3450
		RulesSize = 1073676291U,
		// Token: 0x04000D7B RID: 3451
		RuleVersion = 1720516610U,
		// Token: 0x04000D7C RID: 3452
		RuleSequence = 1719009283U,
		// Token: 0x04000D7D RID: 3453
		RuleState = 1719074819U,
		// Token: 0x04000D7E RID: 3454
		RuleUserFlags = 1719140355U,
		// Token: 0x04000D7F RID: 3455
		RuleName = 1719795743U,
		// Token: 0x04000D80 RID: 3456
		RuleLevel = 1719861251U,
		// Token: 0x04000D81 RID: 3457
		RuleProviderData = 1719927042U,
		// Token: 0x04000D82 RID: 3458
		RuleActions = 1719664894U,
		// Token: 0x04000D83 RID: 3459
		RuleCondition = 1719206141U,
		// Token: 0x04000D84 RID: 3460
		RuleFolderEntryID = 1716584706U,
		// Token: 0x04000D85 RID: 3461
		RuleProvider = 1719730207U,
		// Token: 0x04000D86 RID: 3462
		RuleMsgVersion = 1710358530U,
		// Token: 0x04000D87 RID: 3463
		RuleID = 1718878228U,
		// Token: 0x04000D88 RID: 3464
		RuleError = 1715994627U,
		// Token: 0x04000D89 RID: 3465
		RuleActionType = 1716060163U,
		// Token: 0x04000D8A RID: 3466
		RuleActionNumber = 1716518915U,
		// Token: 0x04000D8B RID: 3467
		HasDeferredActionMessage = 1072300043U,
		// Token: 0x04000D8C RID: 3468
		DeferredActionMessageBackPatched = 1715929099U,
		// Token: 0x04000D8D RID: 3469
		DAMReferenceMessageEntryID = 910295298U,
		// Token: 0x04000D8E RID: 3470
		RuleIDs = 1718944002U,
		// Token: 0x04000D8F RID: 3471
		ClientActions = 1715798274U,
		// Token: 0x04000D90 RID: 3472
		ELCPolicyIds = 1731330078U,
		// Token: 0x04000D91 RID: 3473
		ELCPolicyComment = 1731395615U,
		// Token: 0x04000D92 RID: 3474
		FolderQuota = 1731264515U,
		// Token: 0x04000D93 RID: 3475
		FolderSize = 1731198979U,
		// Token: 0x04000D94 RID: 3476
		FolderSizeExtended = 1731198996U,
		// Token: 0x04000D95 RID: 3477
		AdminFolderFlags = 1731002371U,
		// Token: 0x04000D96 RID: 3478
		PolicyTag = 806945026U,
		// Token: 0x04000D97 RID: 3479
		RetentionPeriod = 807010307U,
		// Token: 0x04000D98 RID: 3480
		StartDateEtc = 807076098U,
		// Token: 0x04000D99 RID: 3481
		RetentionDate = 807141440U,
		// Token: 0x04000D9A RID: 3482
		RetentionFlags = 807206915U,
		// Token: 0x04000D9B RID: 3483
		ArchiveTag = 806879490U,
		// Token: 0x04000D9C RID: 3484
		ArchivePeriod = 807272451U,
		// Token: 0x04000D9D RID: 3485
		ArchiveDate = 807338048U,
		// Token: 0x04000D9E RID: 3486
		ImapInternalDate = 1710555200U,
		// Token: 0x04000D9F RID: 3487
		NextArticleId = 1733361667U,
		// Token: 0x04000DA0 RID: 3488
		ImapLastArticleId = 1733427203U,
		// Token: 0x04000DA1 RID: 3489
		ImapId = 237961219U,
		// Token: 0x04000DA2 RID: 3490
		OriginalSourceServerVersion = 238157826U,
		// Token: 0x04000DA3 RID: 3491
		SpamConfidenceLevel = 1081475075U,
		// Token: 0x04000DA4 RID: 3492
		SenderIdStatus = 1081671683U,
		// Token: 0x04000DA5 RID: 3493
		PurportedSenderDomain = 1082327071U,
		// Token: 0x04000DA6 RID: 3494
		DeliverAsRead = 1476788235U,
		// Token: 0x04000DA7 RID: 3495
		SourceKey = 1709179138U,
		// Token: 0x04000DA8 RID: 3496
		ParentSourceKey = 1709244674U,
		// Token: 0x04000DA9 RID: 3497
		ChangeKey = 1709310210U,
		// Token: 0x04000DAA RID: 3498
		PredecessorChangeList = 1709375746U,
		// Token: 0x04000DAB RID: 3499
		ChangeType = 1733296130U,
		// Token: 0x04000DAC RID: 3500
		Cn = 1738801172U,
		// Token: 0x04000DAD RID: 3501
		Mid = 1732902932U,
		// Token: 0x04000DAE RID: 3502
		Associated = 1739194379U,
		// Token: 0x04000DAF RID: 3503
		ReadCn = 1744699412U,
		// Token: 0x04000DB0 RID: 3504
		ReadCnNew = 1744699650U,
		// Token: 0x04000DB1 RID: 3505
		LTID = 1733820674U,
		// Token: 0x04000DB2 RID: 3506
		EventMask = 1745354755U,
		// Token: 0x04000DB3 RID: 3507
		EventMailboxGuid = 1735000322U,
		// Token: 0x04000DB4 RID: 3508
		DocumentId = 1746206723U,
		// Token: 0x04000DB5 RID: 3509
		MailboxNumber = 1746862083U,
		// Token: 0x04000DB6 RID: 3510
		BeingDeleted = 1728184331U,
		// Token: 0x04000DB7 RID: 3511
		FolderCDN = 1737294082U,
		// Token: 0x04000DB8 RID: 3512
		MessageAgeLimit = 1728118787U,
		// Token: 0x04000DB9 RID: 3513
		ModifiedCount = 1744175107U,
		// Token: 0x04000DBA RID: 3514
		DeleteState = 1744240643U,
		// Token: 0x04000DBB RID: 3515
		AdminDisplayName = 1727922207U,
		// Token: 0x04000DBC RID: 3516
		LastAccessTime = 1722351680U,
		// Token: 0x04000DBD RID: 3517
		LastUserAccessTime = 1747976256U,
		// Token: 0x04000DBE RID: 3518
		LastUserModificationTime = 1748041792U,
		// Token: 0x04000DBF RID: 3519
		AssocMessageSizeExtended = 1723072532U,
		// Token: 0x04000DC0 RID: 3520
		FolderPathName = 1723138078U,
		// Token: 0x04000DC1 RID: 3521
		OwnerCount = 1723203587U,
		// Token: 0x04000DC2 RID: 3522
		ContactCount = 1723269123U,
		// Token: 0x04000DC3 RID: 3523
		MessageAudioNotes = 2080440351U,
		// Token: 0x04000DC4 RID: 3524
		UserPhotoCacheId = 2082078723U,
		// Token: 0x04000DC5 RID: 3525
		UserPhotoPreviewCacheId = 2082144259U,
		// Token: 0x04000DC6 RID: 3526
		OscSyncEnabledOnServer = 2082734091U,
		// Token: 0x04000DC7 RID: 3527
		InternetCPID = 1071513603U,
		// Token: 0x04000DC8 RID: 3528
		StorageQuotaLimit = 1073020931U,
		// Token: 0x04000DC9 RID: 3529
		ExcessStorageUsed = 1073086467U,
		// Token: 0x04000DCA RID: 3530
		SvrGeneratingQuotaMsg = 1073152031U,
		// Token: 0x04000DCB RID: 3531
		PrimaryMbxOverQuota = 1069678603U,
		// Token: 0x04000DCC RID: 3532
		LastConflict = 1070137602U,
		// Token: 0x04000DCD RID: 3533
		QuotaType = 1073610755U,
		// Token: 0x04000DCE RID: 3534
		IsPublicFolderQuotaMessage = 1073676299U,
		// Token: 0x04000DCF RID: 3535
		SecureSubmitFlags = 1707474947U,
		// Token: 0x04000DD0 RID: 3536
		PropertyGroupMapping = 1731461378U,
		// Token: 0x04000DD1 RID: 3537
		PropertyGroupMappingId = 1731461123U,
		// Token: 0x04000DD2 RID: 3538
		PropertyGroupInformation = 1732116738U,
		// Token: 0x04000DD3 RID: 3539
		PropertyGroupChangeMask = 1732116483U,
		// Token: 0x04000DD4 RID: 3540
		SearchRestriction = 1736704258U,
		// Token: 0x04000DD5 RID: 3541
		ViewRestriction = 1739587837U,
		// Token: 0x04000DD6 RID: 3542
		LCIDRestriction = 1736966147U,
		// Token: 0x04000DD7 RID: 3543
		LCID = 1735262211U,
		// Token: 0x04000DD8 RID: 3544
		ViewAccessTime = 1743978560U,
		// Token: 0x04000DD9 RID: 3545
		SortOrder = 1734410498U,
		// Token: 0x04000DDA RID: 3546
		CategCount = 1755185155U,
		// Token: 0x04000DDB RID: 3547
		SoftDeletedFilter = 1746468875U,
		// Token: 0x04000DDC RID: 3548
		AssociatedFilter = 1746534411U,
		// Token: 0x04000DDD RID: 3549
		ConversationsFilter = 1746599947U,
		// Token: 0x04000DDE RID: 3550
		ViewCoveringPropertyTags = 1743917059U,
		// Token: 0x04000DDF RID: 3551
		ISCViewFilter = 1744044043U,
		// Token: 0x04000DE0 RID: 3552
		DVUIdLowest = 1755054083U,
		// Token: 0x04000DE1 RID: 3553
		DVUIdHighest = 1755119619U,
		// Token: 0x04000DE2 RID: 3554
		InstanceGuid = 1746731080U,
		// Token: 0x04000DE3 RID: 3555
		InferenceActivityId = 1746927619U,
		// Token: 0x04000DE4 RID: 3556
		InferenceClientId = 1746993155U,
		// Token: 0x04000DE5 RID: 3557
		InferenceItemId = 1747058946U,
		// Token: 0x04000DE6 RID: 3558
		InferenceTimeStamp = 1747124288U,
		// Token: 0x04000DE7 RID: 3559
		InferenceWindowId = 1747189832U,
		// Token: 0x04000DE8 RID: 3560
		InferenceSessionId = 1747255368U,
		// Token: 0x04000DE9 RID: 3561
		InferenceFolderId = 1747321090U,
		// Token: 0x04000DEA RID: 3562
		InferenceOofEnabled = 1747386379U,
		// Token: 0x04000DEB RID: 3563
		InferenceDeleteType = 1747451907U,
		// Token: 0x04000DEC RID: 3564
		InferenceBrowser = 1747517471U,
		// Token: 0x04000DED RID: 3565
		InferenceLocaleId = 1747582979U,
		// Token: 0x04000DEE RID: 3566
		InferenceLocation = 1747648543U,
		// Token: 0x04000DEF RID: 3567
		InferenceConversationId = 1747714306U,
		// Token: 0x04000DF0 RID: 3568
		InferenceIpAddress = 1747779615U,
		// Token: 0x04000DF1 RID: 3569
		InferenceTimeZone = 1747845151U,
		// Token: 0x04000DF2 RID: 3570
		InferenceCategory = 1747910687U,
		// Token: 0x04000DF3 RID: 3571
		InferenceAttachmentId = 1747976450U,
		// Token: 0x04000DF4 RID: 3572
		InferenceGlobalObjectId = 1748041986U,
		// Token: 0x04000DF5 RID: 3573
		InferenceModuleSelected = 1748107267U,
		// Token: 0x04000DF6 RID: 3574
		InferenceLayoutType = 1748172831U,
		// Token: 0x04000DF7 RID: 3575
		ConversationMvFrom = 1753223199U,
		// Token: 0x04000DF8 RID: 3576
		ConversationMvFromMailboxWide = 1753288735U,
		// Token: 0x04000DF9 RID: 3577
		ConversationMvTo = 1753354271U,
		// Token: 0x04000DFA RID: 3578
		ConversationMvToMailboxWide = 1753419807U,
		// Token: 0x04000DFB RID: 3579
		ConversationMsgDeliveryTime = 1753481280U,
		// Token: 0x04000DFC RID: 3580
		ConversationMsgDeliveryTimeMailboxWide = 1753546816U,
		// Token: 0x04000DFD RID: 3581
		ConversationCategories = 1753616415U,
		// Token: 0x04000DFE RID: 3582
		ConversationCategoriesMailboxWide = 1753681951U,
		// Token: 0x04000DFF RID: 3583
		ConversationFlagStatus = 1753743363U,
		// Token: 0x04000E00 RID: 3584
		ConversationFlagStatusMailboxWide = 1753808899U,
		// Token: 0x04000E01 RID: 3585
		ConversationFlagCompleteTime = 1753874496U,
		// Token: 0x04000E02 RID: 3586
		ConversationFlagCompleteTimeMailboxWide = 1753940032U,
		// Token: 0x04000E03 RID: 3587
		ConversationHasAttach = 1754005515U,
		// Token: 0x04000E04 RID: 3588
		ConversationHasAttachMailboxWide = 1754071051U,
		// Token: 0x04000E05 RID: 3589
		ConversationContentCount = 1754136579U,
		// Token: 0x04000E06 RID: 3590
		ConversationContentCountMailboxWide = 1754202115U,
		// Token: 0x04000E07 RID: 3591
		ConversationContentUnread = 1754267651U,
		// Token: 0x04000E08 RID: 3592
		ConversationContentUnreadMailboxWide = 1754333187U,
		// Token: 0x04000E09 RID: 3593
		ConversationMessageSize = 1754398723U,
		// Token: 0x04000E0A RID: 3594
		ConversationMessageSizeMailboxWide = 1754464259U,
		// Token: 0x04000E0B RID: 3595
		ConversationMessageClasses = 1754533919U,
		// Token: 0x04000E0C RID: 3596
		ConversationMessageClassesMailboxWide = 1754599455U,
		// Token: 0x04000E0D RID: 3597
		ConversationReplyForwardState = 1754660867U,
		// Token: 0x04000E0E RID: 3598
		ConversationReplyForwardStateMailboxWide = 1754726403U,
		// Token: 0x04000E0F RID: 3599
		ConversationImportance = 1754791939U,
		// Token: 0x04000E10 RID: 3600
		ConversationImportanceMailboxWide = 1754857475U,
		// Token: 0x04000E11 RID: 3601
		ConversationMvFromUnread = 1754927135U,
		// Token: 0x04000E12 RID: 3602
		ConversationMvFromUnreadMailboxWide = 1754992671U,
		// Token: 0x04000E13 RID: 3603
		ConversationMvItemIds = 1755320578U,
		// Token: 0x04000E14 RID: 3604
		ConversationMvItemIdsMailboxWide = 1755386114U,
		// Token: 0x04000E15 RID: 3605
		ConversationHasIrm = 1755447307U,
		// Token: 0x04000E16 RID: 3606
		ConversationHasIrmMailboxWide = 1755512843U,
		// Token: 0x04000E17 RID: 3607
		ConversationInferredImportanceInternal = 1755971589U,
		// Token: 0x04000E18 RID: 3608
		ConversationInferredImportanceOverride = 1756037123U,
		// Token: 0x04000E19 RID: 3609
		ConversationInferredUnimportanceInternal = 1756102661U,
		// Token: 0x04000E1A RID: 3610
		ConversationGlobalInferredImportanceInternal = 1756168197U,
		// Token: 0x04000E1B RID: 3611
		ConversationGlobalInferredImportanceOverride = 1756233731U,
		// Token: 0x04000E1C RID: 3612
		ConversationGlobalInferredUnimportanceInternal = 1756299269U,
		// Token: 0x04000E1D RID: 3613
		ConversationGroupingActions = 1757286402U,
		// Token: 0x04000E1E RID: 3614
		ConversationGroupingActionsMailboxWide = 1757351938U,
		// Token: 0x04000E1F RID: 3615
		ConversationPredictedActionsSummary = 1757413379U,
		// Token: 0x04000E20 RID: 3616
		ConversationPredictedActionsSummaryMailboxWide = 1757478915U,
		// Token: 0x04000E21 RID: 3617
		ConversationHasClutter = 1757544459U,
		// Token: 0x04000E22 RID: 3618
		ConversationHasClutterMailboxWide = 1757609995U,
		// Token: 0x04000E23 RID: 3619
		PersonCompanyNameMailboxWide = 1755578399U,
		// Token: 0x04000E24 RID: 3620
		PersonDisplayNameMailboxWide = 1755643935U,
		// Token: 0x04000E25 RID: 3621
		PersonGivenNameMailboxWide = 1755709471U,
		// Token: 0x04000E26 RID: 3622
		PersonSurnameMailboxWide = 1755775007U,
		// Token: 0x04000E27 RID: 3623
		PersonFileAsMailboxWide = 1756364831U,
		// Token: 0x04000E28 RID: 3624
		PersonRelevanceScoreMailboxWide = 1756430339U,
		// Token: 0x04000E29 RID: 3625
		PersonHomeCityMailboxWide = 1756561439U,
		// Token: 0x04000E2A RID: 3626
		PersonCreationTimeMailboxWide = 1756627008U,
		// Token: 0x04000E2B RID: 3627
		PersonWorkCityMailboxWide = 1757282335U,
		// Token: 0x04000E2C RID: 3628
		PersonDisplayNameFirstLastMailboxWide = 1757347871U,
		// Token: 0x04000E2D RID: 3629
		PersonDisplayNameLastFirstMailboxWide = 1757413407U,
		// Token: 0x04000E2E RID: 3630
		TransportSyncSubscriptionListTimestamp = 1747714112U,
		// Token: 0x04000E2F RID: 3631
		TransportRulesSnapshot = 915407106U,
		// Token: 0x04000E30 RID: 3632
		TransportRulesSnapshotId = 915472456U,
		// Token: 0x04000E31 RID: 3633
		DatabaseGuid = 1735000322U,
		// Token: 0x04000E32 RID: 3634
		MessageSizeExtendedLastModificationTime = 1748041792U,
		// Token: 0x04000E33 RID: 3635
		DeletedMessageSizeExtendedLastModificationTime = 2080702528U,
		// Token: 0x04000E34 RID: 3636
		ExchangeObjectId = 1757675778U,
		// Token: 0x04000E35 RID: 3637
		HomeMdb = 2147876877U,
		// Token: 0x04000E36 RID: 3638
		MemberOf = 2148007949U,
		// Token: 0x04000E37 RID: 3639
		Members = 2148073485U,
		// Token: 0x04000E38 RID: 3640
		ManagedBy = 2148270093U,
		// Token: 0x04000E39 RID: 3641
		ProxyAddresses = 2148470815U,
		// Token: 0x04000E3A RID: 3642
		GrantSendOnBehalfTo = 2148859917U,
		// Token: 0x04000E3B RID: 3643
		PfContacts = 2151157791U,
		// Token: 0x04000E3C RID: 3644
		ObjectDistinguishedName = 2151415839U,
		// Token: 0x04000E3D RID: 3645
		HideDLMembership = 2159542283U,
		// Token: 0x04000E3E RID: 3646
		Certificate = 2355761410U,
		// Token: 0x04000E3F RID: 3647
		ObjectGuid = 2355953922U,
		// Token: 0x04000E40 RID: 3648
		ThumbnailPhoto = 2359165186U,
		// Token: 0x04000E41 RID: 3649
		AbIsMaster = 4294639627U,
		// Token: 0x04000E42 RID: 3650
		AbParentEntryId = 4294705410U,
		// Token: 0x04000E43 RID: 3651
		AbContainerId = 4294770691U,
		// Token: 0x04000E44 RID: 3652
		AddressBookRoomContainers = 2358644766U,
		// Token: 0x04000E45 RID: 3653
		AbNetworkAddress = 2171605022U,
		// Token: 0x04000E46 RID: 3654
		IsIntegJobMailboxGuid = 268435528U,
		// Token: 0x04000E47 RID: 3655
		IsIntegJobGuid = 268501064U,
		// Token: 0x04000E48 RID: 3656
		IsIntegJobFlags = 268566531U,
		// Token: 0x04000E49 RID: 3657
		IsIntegJobTask = 268632067U,
		// Token: 0x04000E4A RID: 3658
		IsIntegJobState = 268697602U,
		// Token: 0x04000E4B RID: 3659
		IsIntegJobCreationTime = 268763200U,
		// Token: 0x04000E4C RID: 3660
		IsIntegJobFinishTime = 268828736U,
		// Token: 0x04000E4D RID: 3661
		IsIntegJobLastExecutionTime = 268894272U,
		// Token: 0x04000E4E RID: 3662
		IsIntegJobCorruptionsDetected = 268959747U,
		// Token: 0x04000E4F RID: 3663
		IsIntegJobCorruptionsFixed = 269025283U,
		// Token: 0x04000E50 RID: 3664
		IsIntegJobRequestGuid = 269090888U,
		// Token: 0x04000E51 RID: 3665
		IsIntegJobProgress = 269156354U,
		// Token: 0x04000E52 RID: 3666
		IsIntegJobCorruptions = 269222146U,
		// Token: 0x04000E53 RID: 3667
		IsIntegJobSource = 269287426U,
		// Token: 0x04000E54 RID: 3668
		IsIntegJobPriority = 269352962U,
		// Token: 0x04000E55 RID: 3669
		IsIntegJobTimeInServer = 269418501U,
		// Token: 0x04000E56 RID: 3670
		IsIntegJobMailboxNumber = 269484035U,
		// Token: 0x04000E57 RID: 3671
		IsIntegJobError = 269549571U,
		// Token: 0x04000E58 RID: 3672
		UserInformationGuid = 805306440U,
		// Token: 0x04000E59 RID: 3673
		UserInformationDisplayName = 805371935U,
		// Token: 0x04000E5A RID: 3674
		UserInformationCreationTime = 805437504U,
		// Token: 0x04000E5B RID: 3675
		UserInformationLastModificationTime = 805503040U,
		// Token: 0x04000E5C RID: 3676
		UserInformationChangeNumber = 805568544U,
		// Token: 0x04000E5D RID: 3677
		UserInformationLastInteractiveLogonTime = 805634112U,
		// Token: 0x04000E5E RID: 3678
		UserInformationActiveSyncAllowedDeviceIDs = 805703711U,
		// Token: 0x04000E5F RID: 3679
		UserInformationActiveSyncBlockedDeviceIDs = 805769247U,
		// Token: 0x04000E60 RID: 3680
		UserInformationActiveSyncDebugLogging = 805830659U,
		// Token: 0x04000E61 RID: 3681
		UserInformationActiveSyncEnabled = 805896203U,
		// Token: 0x04000E62 RID: 3682
		UserInformationAdminDisplayName = 805961759U,
		// Token: 0x04000E63 RID: 3683
		UserInformationAggregationSubscriptionCredential = 806031391U,
		// Token: 0x04000E64 RID: 3684
		UserInformationAllowArchiveAddressSync = 806092811U,
		// Token: 0x04000E65 RID: 3685
		UserInformationAltitude = 806158339U,
		// Token: 0x04000E66 RID: 3686
		UserInformationAntispamBypassEnabled = 806223883U,
		// Token: 0x04000E67 RID: 3687
		UserInformationArchiveDomain = 806289439U,
		// Token: 0x04000E68 RID: 3688
		UserInformationArchiveGuid = 806355016U,
		// Token: 0x04000E69 RID: 3689
		UserInformationArchiveName = 806424607U,
		// Token: 0x04000E6A RID: 3690
		UserInformationArchiveQuota = 806486047U,
		// Token: 0x04000E6B RID: 3691
		UserInformationArchiveRelease = 806551583U,
		// Token: 0x04000E6C RID: 3692
		UserInformationArchiveStatus = 806617091U,
		// Token: 0x04000E6D RID: 3693
		UserInformationArchiveWarningQuota = 806682655U,
		// Token: 0x04000E6E RID: 3694
		UserInformationAssistantName = 806748191U,
		// Token: 0x04000E6F RID: 3695
		UserInformationBirthdate = 806813760U,
		// Token: 0x04000E70 RID: 3696
		UserInformationBypassNestedModerationEnabled = 806879243U,
		// Token: 0x04000E71 RID: 3697
		UserInformationC = 806944799U,
		// Token: 0x04000E72 RID: 3698
		UserInformationCalendarLoggingQuota = 807010335U,
		// Token: 0x04000E73 RID: 3699
		UserInformationCalendarRepairDisabled = 807075851U,
		// Token: 0x04000E74 RID: 3700
		UserInformationCalendarVersionStoreDisabled = 807141387U,
		// Token: 0x04000E75 RID: 3701
		UserInformationCity = 807206943U,
		// Token: 0x04000E76 RID: 3702
		UserInformationCountry = 807272479U,
		// Token: 0x04000E77 RID: 3703
		UserInformationCountryCode = 807337987U,
		// Token: 0x04000E78 RID: 3704
		UserInformationCountryOrRegion = 807403551U,
		// Token: 0x04000E79 RID: 3705
		UserInformationDefaultMailTip = 807469087U,
		// Token: 0x04000E7A RID: 3706
		UserInformationDeliverToMailboxAndForward = 807534603U,
		// Token: 0x04000E7B RID: 3707
		UserInformationDescription = 807604255U,
		// Token: 0x04000E7C RID: 3708
		UserInformationDisabledArchiveGuid = 807665736U,
		// Token: 0x04000E7D RID: 3709
		UserInformationDowngradeHighPriorityMessagesEnabled = 807731211U,
		// Token: 0x04000E7E RID: 3710
		UserInformationECPEnabled = 807796747U,
		// Token: 0x04000E7F RID: 3711
		UserInformationEmailAddressPolicyEnabled = 807862283U,
		// Token: 0x04000E80 RID: 3712
		UserInformationEwsAllowEntourage = 807927819U,
		// Token: 0x04000E81 RID: 3713
		UserInformationEwsAllowMacOutlook = 807993355U,
		// Token: 0x04000E82 RID: 3714
		UserInformationEwsAllowOutlook = 808058891U,
		// Token: 0x04000E83 RID: 3715
		UserInformationEwsApplicationAccessPolicy = 808124419U,
		// Token: 0x04000E84 RID: 3716
		UserInformationEwsEnabled = 808189955U,
		// Token: 0x04000E85 RID: 3717
		UserInformationEwsExceptions = 808259615U,
		// Token: 0x04000E86 RID: 3718
		UserInformationEwsWellKnownApplicationAccessPolicies = 808325151U,
		// Token: 0x04000E87 RID: 3719
		UserInformationExchangeGuid = 808386632U,
		// Token: 0x04000E88 RID: 3720
		UserInformationExternalOofOptions = 808452099U,
		// Token: 0x04000E89 RID: 3721
		UserInformationFirstName = 808517663U,
		// Token: 0x04000E8A RID: 3722
		UserInformationForwardingSmtpAddress = 808583199U,
		// Token: 0x04000E8B RID: 3723
		UserInformationGender = 808648735U,
		// Token: 0x04000E8C RID: 3724
		UserInformationGenericForwardingAddress = 808714271U,
		// Token: 0x04000E8D RID: 3725
		UserInformationGeoCoordinates = 808779807U,
		// Token: 0x04000E8E RID: 3726
		UserInformationHABSeniorityIndex = 808845315U,
		// Token: 0x04000E8F RID: 3727
		UserInformationHasActiveSyncDevicePartnership = 808910859U,
		// Token: 0x04000E90 RID: 3728
		UserInformationHiddenFromAddressListsEnabled = 808976395U,
		// Token: 0x04000E91 RID: 3729
		UserInformationHiddenFromAddressListsValue = 809041931U,
		// Token: 0x04000E92 RID: 3730
		UserInformationHomePhone = 809107487U,
		// Token: 0x04000E93 RID: 3731
		UserInformationImapEnabled = 809173003U,
		// Token: 0x04000E94 RID: 3732
		UserInformationImapEnableExactRFC822Size = 809238539U,
		// Token: 0x04000E95 RID: 3733
		UserInformationImapForceICalForCalendarRetrievalOption = 809304075U,
		// Token: 0x04000E96 RID: 3734
		UserInformationImapMessagesRetrievalMimeFormat = 809369603U,
		// Token: 0x04000E97 RID: 3735
		UserInformationImapProtocolLoggingEnabled = 809435139U,
		// Token: 0x04000E98 RID: 3736
		UserInformationImapSuppressReadReceipt = 809500683U,
		// Token: 0x04000E99 RID: 3737
		UserInformationImapUseProtocolDefaults = 809566219U,
		// Token: 0x04000E9A RID: 3738
		UserInformationIncludeInGarbageCollection = 809631755U,
		// Token: 0x04000E9B RID: 3739
		UserInformationInitials = 809697311U,
		// Token: 0x04000E9C RID: 3740
		UserInformationInPlaceHolds = 809766943U,
		// Token: 0x04000E9D RID: 3741
		UserInformationInternalOnly = 809828363U,
		// Token: 0x04000E9E RID: 3742
		UserInformationInternalUsageLocation = 809893919U,
		// Token: 0x04000E9F RID: 3743
		UserInformationInternetEncoding = 809959427U,
		// Token: 0x04000EA0 RID: 3744
		UserInformationIsCalculatedTargetAddress = 810024971U,
		// Token: 0x04000EA1 RID: 3745
		UserInformationIsExcludedFromServingHierarchy = 810090507U,
		// Token: 0x04000EA2 RID: 3746
		UserInformationIsHierarchyReady = 810156043U,
		// Token: 0x04000EA3 RID: 3747
		UserInformationIsInactiveMailbox = 810221579U,
		// Token: 0x04000EA4 RID: 3748
		UserInformationIsSoftDeletedByDisable = 810287115U,
		// Token: 0x04000EA5 RID: 3749
		UserInformationIsSoftDeletedByRemove = 810352651U,
		// Token: 0x04000EA6 RID: 3750
		UserInformationIssueWarningQuota = 810418207U,
		// Token: 0x04000EA7 RID: 3751
		UserInformationJournalArchiveAddress = 810483743U,
		// Token: 0x04000EA8 RID: 3752
		UserInformationLanguages = 810553375U,
		// Token: 0x04000EA9 RID: 3753
		UserInformationLastExchangeChangedTime = 810614848U,
		// Token: 0x04000EAA RID: 3754
		UserInformationLastName = 810680351U,
		// Token: 0x04000EAB RID: 3755
		UserInformationLatitude = 810745859U,
		// Token: 0x04000EAC RID: 3756
		UserInformationLEOEnabled = 810811403U,
		// Token: 0x04000EAD RID: 3757
		UserInformationLocaleID = 810881027U,
		// Token: 0x04000EAE RID: 3758
		UserInformationLongitude = 810942467U,
		// Token: 0x04000EAF RID: 3759
		UserInformationMacAttachmentFormat = 811008003U,
		// Token: 0x04000EB0 RID: 3760
		UserInformationMailboxContainerGuid = 811073608U,
		// Token: 0x04000EB1 RID: 3761
		UserInformationMailboxMoveBatchName = 811139103U,
		// Token: 0x04000EB2 RID: 3762
		UserInformationMailboxMoveRemoteHostName = 811204639U,
		// Token: 0x04000EB3 RID: 3763
		UserInformationMailboxMoveStatus = 811270147U,
		// Token: 0x04000EB4 RID: 3764
		UserInformationMailboxRelease = 811335711U,
		// Token: 0x04000EB5 RID: 3765
		UserInformationMailTipTranslations = 811405343U,
		// Token: 0x04000EB6 RID: 3766
		UserInformationMAPIBlockOutlookNonCachedMode = 811466763U,
		// Token: 0x04000EB7 RID: 3767
		UserInformationMAPIBlockOutlookRpcHttp = 811532299U,
		// Token: 0x04000EB8 RID: 3768
		UserInformationMAPIBlockOutlookVersions = 811597855U,
		// Token: 0x04000EB9 RID: 3769
		UserInformationMAPIEnabled = 811663371U,
		// Token: 0x04000EBA RID: 3770
		UserInformationMapiRecipient = 811728907U,
		// Token: 0x04000EBB RID: 3771
		UserInformationMaxBlockedSenders = 811794435U,
		// Token: 0x04000EBC RID: 3772
		UserInformationMaxReceiveSize = 811859999U,
		// Token: 0x04000EBD RID: 3773
		UserInformationMaxSafeSenders = 811925507U,
		// Token: 0x04000EBE RID: 3774
		UserInformationMaxSendSize = 811991071U,
		// Token: 0x04000EBF RID: 3775
		UserInformationMemberName = 812056607U,
		// Token: 0x04000EC0 RID: 3776
		UserInformationMessageBodyFormat = 812122115U,
		// Token: 0x04000EC1 RID: 3777
		UserInformationMessageFormat = 812187651U,
		// Token: 0x04000EC2 RID: 3778
		UserInformationMessageTrackingReadStatusDisabled = 812253195U,
		// Token: 0x04000EC3 RID: 3779
		UserInformationMobileFeaturesEnabled = 812318723U,
		// Token: 0x04000EC4 RID: 3780
		UserInformationMobilePhone = 812384287U,
		// Token: 0x04000EC5 RID: 3781
		UserInformationModerationFlags = 812449795U,
		// Token: 0x04000EC6 RID: 3782
		UserInformationNotes = 812515359U,
		// Token: 0x04000EC7 RID: 3783
		UserInformationOccupation = 812580895U,
		// Token: 0x04000EC8 RID: 3784
		UserInformationOpenDomainRoutingDisabled = 812646411U,
		// Token: 0x04000EC9 RID: 3785
		UserInformationOtherHomePhone = 812716063U,
		// Token: 0x04000ECA RID: 3786
		UserInformationOtherMobile = 812781599U,
		// Token: 0x04000ECB RID: 3787
		UserInformationOtherTelephone = 812847135U,
		// Token: 0x04000ECC RID: 3788
		UserInformationOWAEnabled = 812908555U,
		// Token: 0x04000ECD RID: 3789
		UserInformationOWAforDevicesEnabled = 812974091U,
		// Token: 0x04000ECE RID: 3790
		UserInformationPager = 813039647U,
		// Token: 0x04000ECF RID: 3791
		UserInformationPersistedCapabilities = 813109251U,
		// Token: 0x04000ED0 RID: 3792
		UserInformationPhone = 813170719U,
		// Token: 0x04000ED1 RID: 3793
		UserInformationPhoneProviderId = 813236255U,
		// Token: 0x04000ED2 RID: 3794
		UserInformationPopEnabled = 813301771U,
		// Token: 0x04000ED3 RID: 3795
		UserInformationPopEnableExactRFC822Size = 813367307U,
		// Token: 0x04000ED4 RID: 3796
		UserInformationPopForceICalForCalendarRetrievalOption = 813432843U,
		// Token: 0x04000ED5 RID: 3797
		UserInformationPopMessagesRetrievalMimeFormat = 813498371U,
		// Token: 0x04000ED6 RID: 3798
		UserInformationPopProtocolLoggingEnabled = 813563907U,
		// Token: 0x04000ED7 RID: 3799
		UserInformationPopSuppressReadReceipt = 813629451U,
		// Token: 0x04000ED8 RID: 3800
		UserInformationPopUseProtocolDefaults = 813694987U,
		// Token: 0x04000ED9 RID: 3801
		UserInformationPostalCode = 813760543U,
		// Token: 0x04000EDA RID: 3802
		UserInformationPostOfficeBox = 813830175U,
		// Token: 0x04000EDB RID: 3803
		UserInformationPreviousExchangeGuid = 813891656U,
		// Token: 0x04000EDC RID: 3804
		UserInformationPreviousRecipientTypeDetails = 813957123U,
		// Token: 0x04000EDD RID: 3805
		UserInformationProhibitSendQuota = 814022687U,
		// Token: 0x04000EDE RID: 3806
		UserInformationProhibitSendReceiveQuota = 814088223U,
		// Token: 0x04000EDF RID: 3807
		UserInformationQueryBaseDNRestrictionEnabled = 814153739U,
		// Token: 0x04000EE0 RID: 3808
		UserInformationRecipientDisplayType = 814219267U,
		// Token: 0x04000EE1 RID: 3809
		UserInformationRecipientLimits = 814284831U,
		// Token: 0x04000EE2 RID: 3810
		UserInformationRecipientSoftDeletedStatus = 814350339U,
		// Token: 0x04000EE3 RID: 3811
		UserInformationRecoverableItemsQuota = 814415903U,
		// Token: 0x04000EE4 RID: 3812
		UserInformationRecoverableItemsWarningQuota = 814481439U,
		// Token: 0x04000EE5 RID: 3813
		UserInformationRegion = 814546975U,
		// Token: 0x04000EE6 RID: 3814
		UserInformationRemotePowerShellEnabled = 814612491U,
		// Token: 0x04000EE7 RID: 3815
		UserInformationRemoteRecipientType = 814678019U,
		// Token: 0x04000EE8 RID: 3816
		UserInformationRequireAllSendersAreAuthenticated = 814743563U,
		// Token: 0x04000EE9 RID: 3817
		UserInformationResetPasswordOnNextLogon = 814809099U,
		// Token: 0x04000EEA RID: 3818
		UserInformationRetainDeletedItemsFor = 814874644U,
		// Token: 0x04000EEB RID: 3819
		UserInformationRetainDeletedItemsUntilBackup = 814940171U,
		// Token: 0x04000EEC RID: 3820
		UserInformationRulesQuota = 815005727U,
		// Token: 0x04000EED RID: 3821
		UserInformationShouldUseDefaultRetentionPolicy = 815071243U,
		// Token: 0x04000EEE RID: 3822
		UserInformationSimpleDisplayName = 815136799U,
		// Token: 0x04000EEF RID: 3823
		UserInformationSingleItemRecoveryEnabled = 815202315U,
		// Token: 0x04000EF0 RID: 3824
		UserInformationStateOrProvince = 815267871U,
		// Token: 0x04000EF1 RID: 3825
		UserInformationStreetAddress = 815333407U,
		// Token: 0x04000EF2 RID: 3826
		UserInformationSubscriberAccessEnabled = 815398923U,
		// Token: 0x04000EF3 RID: 3827
		UserInformationTextEncodedORAddress = 815464479U,
		// Token: 0x04000EF4 RID: 3828
		UserInformationTextMessagingState = 815534111U,
		// Token: 0x04000EF5 RID: 3829
		UserInformationTimezone = 815595551U,
		// Token: 0x04000EF6 RID: 3830
		UserInformationUCSImListMigrationCompleted = 815661067U,
		// Token: 0x04000EF7 RID: 3831
		UserInformationUpgradeDetails = 815726623U,
		// Token: 0x04000EF8 RID: 3832
		UserInformationUpgradeMessage = 815792159U,
		// Token: 0x04000EF9 RID: 3833
		UserInformationUpgradeRequest = 815857667U,
		// Token: 0x04000EFA RID: 3834
		UserInformationUpgradeStage = 815923203U,
		// Token: 0x04000EFB RID: 3835
		UserInformationUpgradeStageTimeStamp = 815988800U,
		// Token: 0x04000EFC RID: 3836
		UserInformationUpgradeStatus = 816054275U,
		// Token: 0x04000EFD RID: 3837
		UserInformationUsageLocation = 816119839U,
		// Token: 0x04000EFE RID: 3838
		UserInformationUseMapiRichTextFormat = 816185347U,
		// Token: 0x04000EFF RID: 3839
		UserInformationUsePreferMessageFormat = 816250891U,
		// Token: 0x04000F00 RID: 3840
		UserInformationUseUCCAuditConfig = 816316427U,
		// Token: 0x04000F01 RID: 3841
		UserInformationWebPage = 816381983U,
		// Token: 0x04000F02 RID: 3842
		UserInformationWhenMailboxCreated = 816447552U,
		// Token: 0x04000F03 RID: 3843
		UserInformationWhenSoftDeleted = 816513088U,
		// Token: 0x04000F04 RID: 3844
		UserInformationBirthdayPrecision = 816578591U,
		// Token: 0x04000F05 RID: 3845
		UserInformationNameVersion = 816644127U,
		// Token: 0x04000F06 RID: 3846
		UserInformationOptInUser = 816709643U,
		// Token: 0x04000F07 RID: 3847
		UserInformationIsMigratedConsumerMailbox = 816775179U,
		// Token: 0x04000F08 RID: 3848
		UserInformationMigrationDryRun = 816840715U,
		// Token: 0x04000F09 RID: 3849
		UserInformationIsPremiumConsumerMailbox = 816906251U,
		// Token: 0x04000F0A RID: 3850
		UserInformationAlternateSupportEmailAddresses = 816971807U,
		// Token: 0x04000F0B RID: 3851
		UserInformationEmailAddresses = 817041439U,
		// Token: 0x04000F0C RID: 3852
		UserInformationMapiHttpEnabled = 819331083U,
		// Token: 0x04000F0D RID: 3853
		UserInformationMAPIBlockOutlookExternalConnectivity = 819396619U,
		// Token: 0x04000F0E RID: 3854
		MessageTableTotalPages = 872480771U,
		// Token: 0x04000F0F RID: 3855
		MessageTableAvailablePages = 872546307U,
		// Token: 0x04000F10 RID: 3856
		OtherTablesTotalPages = 872611843U,
		// Token: 0x04000F11 RID: 3857
		OtherTablesAvailablePages = 872677379U,
		// Token: 0x04000F12 RID: 3858
		AttachmentTableTotalPages = 872742915U,
		// Token: 0x04000F13 RID: 3859
		AttachmentTableAvailablePages = 872808451U,
		// Token: 0x04000F14 RID: 3860
		LegacyShortcutsFolderEntryId = 1714422018U,
		// Token: 0x04000F15 RID: 3861
		MailboxDSGuidGuid = 1728512072U,
		// Token: 0x04000F16 RID: 3862
		UnifiedMailboxGuidGuid = 1728577608U,
		// Token: 0x04000F17 RID: 3863
		MailboxPartitionMailboxGuids = 872943688U,
		// Token: 0x04000F18 RID: 3864
		InternalAccess = 921763851U,
		// Token: 0x04000F19 RID: 3865
		AddressType = 805437471U,
		// Token: 0x04000F1A RID: 3866
		SourceFid = 241106964U
	}
}
