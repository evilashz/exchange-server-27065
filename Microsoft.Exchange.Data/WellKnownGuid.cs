using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E1 RID: 481
	internal static class WellKnownGuid
	{
		// Token: 0x040009DA RID: 2522
		private const string systemWkGuidString = "{F3301DAB-8876-D111-ADED-00C04FD8D5CD}";

		// Token: 0x040009DB RID: 2523
		private const string domainControllersWkGuidString = "{FFB261A3-D2FF-D111-AA4B-00C04FD7D83A}";

		// Token: 0x040009DC RID: 2524
		private const string lostAndFoundContainerWkGuidString = "{b75381ab-8876-d111-aded-00c04fd8d5cd}";

		// Token: 0x040009DD RID: 2525
		private const string usersWkGuidString = "{15CAD1A9-8876-D111-ADED-00C04FD8D5CD}";

		// Token: 0x040009DE RID: 2526
		private const string exsWkGuidString = "{6C01D2A7-F083-4503-8132-789EEB127B84}";

		// Token: 0x040009DF RID: 2527
		private const string masWkGuidString = "{CF2E5202-8599-4A98-9232-056FC704CC8B}";

		// Token: 0x040009E0 RID: 2528
		private const string eoaWkGuidString_E12 = "{3D604B35-D992-4155-AAFD-8C0AE688EA0F}";

		// Token: 0x040009E1 RID: 2529
		private const string eoaWkGuidString = "{29A962C2-91D6-4AB7-9E06-8728F8F842EA}";

		// Token: 0x040009E2 RID: 2530
		private const string emaWkGuidString_E12 = "{D998D80B-0839-4210-B77B-F4A555E07FA1}";

		// Token: 0x040009E3 RID: 2531
		private const string emaWkGuidString = "{1DC472DB-5849-4D0A-B304-FE6981E56297}";

		// Token: 0x040009E4 RID: 2532
		private const string epaWkGuidString_E12 = "{73000302-5915-4dce-A330-91DD24F58231}";

		// Token: 0x040009E5 RID: 2533
		private const string epaWkGuidString = "{7B41FA45-7435-4EDC-929B-C4B059699792}";

		// Token: 0x040009E6 RID: 2534
		private const string eraWkGuidString_E12 = "{660113DC-542F-4574-B9C4-90A52961F8FC}";

		// Token: 0x040009E7 RID: 2535
		private const string eraWkGuidString = "{D3399E1A-BE5A-4757-B979-FFC0C6E5EA26}";

		// Token: 0x040009E8 RID: 2536
		private const string e3iWkGuidString = "{3F965B9C-F167-4b4a-936C-B8EFB19C4784}";

		// Token: 0x040009E9 RID: 2537
		private const string etsWkGuidString = "{586a87ea-6ddb-4cd0-9006-939818f800eb}";

		// Token: 0x040009EA RID: 2538
		private const string ewpWkGuidString = "{11d0174c-be7e-4266-afae-e03bc66d381f}";

		// Token: 0x040009EB RID: 2539
		private const string efomgWkGuidString = "{a0aad226-2daf-4978-b5a1-d2debd168d8f}";

		// Token: 0x040009EC RID: 2540
		private const string eopsWkGuidString = "{E8A7E650-5F72-440e-9D77-E250A6C0E8F9}";

		// Token: 0x040009ED RID: 2541
		private const string eahoWkGuidString = "{70bf766e-de1c-4776-b193-758d1306c8fb}";

		// Token: 0x040009EE RID: 2542
		private const string configDeletedObjectsWkGuidString = "{80EAE218-4F68-D211-B9AA-00C04F79F805}";

		// Token: 0x040009EF RID: 2543
		private const string exchangeInfoPropSetGuidString = "{1f298a89-de98-47b8-b5cd-572Ad53d267e}";

		// Token: 0x040009F0 RID: 2544
		private const string exchangePersonalInfoPropSetGuidString = "{B1B3A417-EC55-4191-B327-B72E33E38AF2}";

		// Token: 0x040009F1 RID: 2545
		private const string publicInfoPropSetGuidString = "{e48d0154-bcf8-11d1-8702-00c04fb96050}";

		// Token: 0x040009F2 RID: 2546
		private const string personalInfoPropSetGuidString = "{77b5b886-944a-11d1-aebd-0000f80367c1}";

		// Token: 0x040009F3 RID: 2547
		private const string userAccountRestrictionsPropSetGuidString = "{4c164200-20c0-11d0-a768-00aa006e0529}";

		// Token: 0x040009F4 RID: 2548
		public const string InPlaceHoldIdentityLegalHoldGuidString = "98E9BABD09A04bcf8455A58C2AA74182";

		// Token: 0x040009F5 RID: 2549
		public const string SendAsExtendedRightGuidString = "ab721a54-1e2f-11d0-9819-00aa0040529b";

		// Token: 0x040009F6 RID: 2550
		public const string ReceiveAsExtendedRightGuidString = "ab721a56-1e2f-11d0-9819-00aa0040529b";

		// Token: 0x040009F7 RID: 2551
		public static readonly Guid SystemWkGuid = new Guid("{F3301DAB-8876-D111-ADED-00C04FD8D5CD}");

		// Token: 0x040009F8 RID: 2552
		public static readonly Guid DomainControllersWkGuid = new Guid("{FFB261A3-D2FF-D111-AA4B-00C04FD7D83A}");

		// Token: 0x040009F9 RID: 2553
		public static readonly Guid LostAndFoundContainerWkGuid = new Guid("{b75381ab-8876-d111-aded-00c04fd8d5cd}");

		// Token: 0x040009FA RID: 2554
		public static readonly Guid UsersWkGuid = new Guid("{15CAD1A9-8876-D111-ADED-00C04FD8D5CD}");

		// Token: 0x040009FB RID: 2555
		public static readonly Guid ExSWkGuid = new Guid("{6C01D2A7-F083-4503-8132-789EEB127B84}");

		// Token: 0x040009FC RID: 2556
		public static readonly Guid MaSWkGuid = new Guid("{CF2E5202-8599-4A98-9232-056FC704CC8B}");

		// Token: 0x040009FD RID: 2557
		public static readonly Guid EoaWkGuid_E12 = new Guid("{3D604B35-D992-4155-AAFD-8C0AE688EA0F}");

		// Token: 0x040009FE RID: 2558
		public static readonly Guid EoaWkGuid = new Guid("{29A962C2-91D6-4AB7-9E06-8728F8F842EA}");

		// Token: 0x040009FF RID: 2559
		public static readonly Guid EmaWkGuid_E12 = new Guid("{D998D80B-0839-4210-B77B-F4A555E07FA1}");

		// Token: 0x04000A00 RID: 2560
		public static readonly Guid EmaWkGuid = new Guid("{1DC472DB-5849-4D0A-B304-FE6981E56297}");

		// Token: 0x04000A01 RID: 2561
		public static readonly Guid EpaWkGuid_E12 = new Guid("{73000302-5915-4dce-A330-91DD24F58231}");

		// Token: 0x04000A02 RID: 2562
		public static readonly Guid EpaWkGuid = new Guid("{7B41FA45-7435-4EDC-929B-C4B059699792}");

		// Token: 0x04000A03 RID: 2563
		public static readonly Guid EraWkGuid_E12 = new Guid("{660113DC-542F-4574-B9C4-90A52961F8FC}");

		// Token: 0x04000A04 RID: 2564
		public static readonly Guid EraWkGuid = new Guid("{D3399E1A-BE5A-4757-B979-FFC0C6E5EA26}");

		// Token: 0x04000A05 RID: 2565
		public static readonly Guid E3iWkGuid = new Guid("{3F965B9C-F167-4b4a-936C-B8EFB19C4784}");

		// Token: 0x04000A06 RID: 2566
		public static readonly Guid EtsWkGuid = new Guid("{586a87ea-6ddb-4cd0-9006-939818f800eb}");

		// Token: 0x04000A07 RID: 2567
		public static readonly Guid EwpWkGuid = new Guid("{11d0174c-be7e-4266-afae-e03bc66d381f}");

		// Token: 0x04000A08 RID: 2568
		public static readonly Guid EfomgWkGuid = new Guid("{a0aad226-2daf-4978-b5a1-d2debd168d8f}");

		// Token: 0x04000A09 RID: 2569
		public static readonly Guid EopsWkGuid = new Guid("{E8A7E650-5F72-440e-9D77-E250A6C0E8F9}");

		// Token: 0x04000A0A RID: 2570
		public static readonly Guid EahoWkGuid = new Guid("{70bf766e-de1c-4776-b193-758d1306c8fb}");

		// Token: 0x04000A0B RID: 2571
		public static readonly Guid ConfigDeletedObjectsWkGuid = new Guid("{80EAE218-4F68-D211-B9AA-00C04F79F805}");

		// Token: 0x04000A0C RID: 2572
		public static readonly Guid ExchangeInfoPropSetGuid = new Guid("{1f298a89-de98-47b8-b5cd-572Ad53d267e}");

		// Token: 0x04000A0D RID: 2573
		public static readonly Guid ExchangePersonalInfoPropSetGuid = new Guid("{B1B3A417-EC55-4191-B327-B72E33E38AF2}");

		// Token: 0x04000A0E RID: 2574
		public static readonly Guid PublicInfoPropSetGuid = new Guid("{e48d0154-bcf8-11d1-8702-00c04fb96050}");

		// Token: 0x04000A0F RID: 2575
		public static readonly Guid PersonalInfoPropSetGuid = new Guid("{77b5b886-944a-11d1-aebd-0000f80367c1}");

		// Token: 0x04000A10 RID: 2576
		public static readonly Guid UserAccountRestrictionsPropSetGuid = new Guid("{4c164200-20c0-11d0-a768-00aa006e0529}");

		// Token: 0x04000A11 RID: 2577
		public static readonly Guid InPlaceHoldIdentityLegalHoldGuid = new Guid("98E9BABD09A04bcf8455A58C2AA74182");

		// Token: 0x04000A12 RID: 2578
		public static readonly Guid CreatePublicFolderExtendedRightGuid = new Guid("cf0b3dc8-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A13 RID: 2579
		public static readonly Guid CreateTopLevelPublicFolderExtendedRightGuid = new Guid("cf4b9d46-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A14 RID: 2580
		public static readonly Guid RIMMailboxAdminsGroupGuid = new Guid("1e6b6d42-7174-4e4b-8de1-0df23acb1c42");

		// Token: 0x04000A15 RID: 2581
		public static readonly Guid RIMMailboxUsersGroupGuid = new Guid("62f35d94-58c7-4003-adda-fc207c1562a7");

		// Token: 0x04000A16 RID: 2582
		public static readonly Guid MailEnablePublicFolderGuid = new Guid("cf899a6a-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A17 RID: 2583
		public static readonly Guid ModifyPublicFolderACLExtendedRightGuid = new Guid("D74A8769-22B9-11d3-AA62-00C04F8EEDD8");

		// Token: 0x04000A18 RID: 2584
		public static readonly Guid ModifyPublicFolderAdminACLExtendedRightGuid = new Guid("D74A876F-22B9-11d3-AA62-00C04F8EEDD8");

		// Token: 0x04000A19 RID: 2585
		public static readonly Guid ModifyPublicFolderDeletedItemRetentionExtendedRightGuid = new Guid("cffe6da4-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A1A RID: 2586
		public static readonly Guid ModifyPublicFolderExpiryExtendedRightGuid = new Guid("cfc7978e-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A1B RID: 2587
		public static readonly Guid ModifyPublicFolderQuotasExtendedRightGuid = new Guid("d03a086e-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A1C RID: 2588
		public static readonly Guid ModifyPublicFolderReplicaListExtendedRightGuid = new Guid("d0780592-afe6-11d2-aa04-00c04f8eedd8");

		// Token: 0x04000A1D RID: 2589
		public static readonly Guid SendAsExtendedRightGuid = new Guid("ab721a54-1e2f-11d0-9819-00aa0040529b");

		// Token: 0x04000A1E RID: 2590
		public static readonly Guid ReceiveAsExtendedRightGuid = new Guid("ab721a56-1e2f-11d0-9819-00aa0040529b");

		// Token: 0x04000A1F RID: 2591
		public static readonly Guid StoreCreateNamedPropertiesExtendedRightGuid = new Guid("D74A8766-22B9-11d3-AA62-00C04F8EEDD8");

		// Token: 0x04000A20 RID: 2592
		public static readonly Guid StoreTransportAccessExtendedRightGuid = new Guid("9fbec2a2-f761-11d9-963d-00065bbd3175");

		// Token: 0x04000A21 RID: 2593
		public static readonly Guid StoreConstrainedDelegationExtendedRightGuid = new Guid("9fbec2a1-f761-11d9-963d-00065bbd3175");

		// Token: 0x04000A22 RID: 2594
		public static readonly Guid StoreReadAccessExtendedRightGuid = new Guid("9fbec2a3-f761-11d9-963d-00065bbd3175");

		// Token: 0x04000A23 RID: 2595
		public static readonly Guid StoreReadWriteAccessExtendedRightGuid = new Guid("9fbec2a4-f761-11d9-963d-00065bbd3175");

		// Token: 0x04000A24 RID: 2596
		public static readonly Guid StoreAdminExtendedRightGuid = new Guid("D74A8762-22B9-11d3-AA62-00C04F8EEDD8");

		// Token: 0x04000A25 RID: 2597
		public static readonly Guid StoreVisibleExtendedRightGuid = new Guid("D74A875E-22B9-11d3-AA62-00C04F8EEDD8");

		// Token: 0x04000A26 RID: 2598
		public static readonly Guid ChangePasswordExtendedRightGuid = new Guid("ab721a53-1e2f-11d0-9819-00aa0040529b");

		// Token: 0x04000A27 RID: 2599
		public static readonly Guid ResetPasswordOnNextLogonExtendedRightGuid = new Guid("00299570-246d-11d0-a768-00aa006e0529");

		// Token: 0x04000A28 RID: 2600
		public static readonly Guid RecipientUpdateExtendedRightGuid = new Guid("165AB2CC-D1B3-4717-9B90-C657E7E57F4D");

		// Token: 0x04000A29 RID: 2601
		public static readonly Guid DownloadOABExtendedRightGuid = new Guid("BD919C7C-2D79-4950-BC9C-E16FD99285E8");

		// Token: 0x04000A2A RID: 2602
		public static readonly Guid EpiImpersonationRightGuid = new Guid("8DB0795C-DF3A-4aca-A97D-100162998DFA");

		// Token: 0x04000A2B RID: 2603
		public static readonly Guid TokenSerializationRightGuid = new Guid("06386F89-BEFB-4e48-BAA1-559FD9221F78");

		// Token: 0x04000A2C RID: 2604
		public static readonly Guid EpiMayImpersonateRightGuid = new Guid("bc39105d-9baa-477c-a34a-997cc25e3d60");

		// Token: 0x04000A2D RID: 2605
		public static readonly Guid DsReplicationSynchronize = new Guid("1131f6ab-9c07-11d1-f79f-00c04fc2dcd2");

		// Token: 0x04000A2E RID: 2606
		public static readonly Guid DsReplicationGetChanges = new Guid("1131f6aa-9c07-11d1-f79f-00c04fc2dcd2");

		// Token: 0x04000A2F RID: 2607
		public static readonly Guid OpenAddressBookRight = new Guid("a1990816-4298-11d1-ade2-00c04fd8d5cd");

		// Token: 0x04000A30 RID: 2608
		public static readonly Guid RgUmManagementWkGuid = new Guid("B7DF0CE8-9756-4993-81C8-98B4DBC5A0C6");

		// Token: 0x04000A31 RID: 2609
		public static readonly Guid RgHelpDeskWkGuid = new Guid("BEC6DDB3-3B2A-4BE8-97EB-2DCE9477E389");

		// Token: 0x04000A32 RID: 2610
		public static readonly Guid RgRecordsManagementWkGuid = new Guid("C932A4BE-1D4E-4E25-AF99-B40573360D5B");

		// Token: 0x04000A33 RID: 2611
		public static readonly Guid RgDiscoveryManagementWkGuid = new Guid("2EDE7FC6-3983-4467-90FB-AFDCA3DFDC95");

		// Token: 0x04000A34 RID: 2612
		public static readonly Guid RgServerManagementWkGuid = new Guid("75E7B84D-B64E-43c1-9565-612E69A80A4F");

		// Token: 0x04000A35 RID: 2613
		public static readonly Guid RgDelegatedSetupWkGuid = new Guid("261928D1-F5D1-445b-866D-1D6B5BD87A09");

		// Token: 0x04000A36 RID: 2614
		public static readonly Guid RgHygieneManagementWkGuid = new Guid("F409B703-F351-43BF-88E3-3495369B6771");

		// Token: 0x04000A37 RID: 2615
		public static readonly Guid RgManagementForestOperatorWkGuid = new Guid("3178BCE1-9934-4fdc-AB62-5FB6A502B820");

		// Token: 0x04000A38 RID: 2616
		public static readonly Guid RgManagementForestTier1SupportWkGuid = new Guid("4B472D3E-1000-4ab7-A9CA-DE5ABDC317D1");

		// Token: 0x04000A39 RID: 2617
		public static readonly Guid RgViewOnlyManagementForestOperatorWkGuid = new Guid("971DDF37-4865-40ec-890A-BF2EC8172E04");

		// Token: 0x04000A3A RID: 2618
		public static readonly Guid RgManagementForestMonitoringWkGuid = new Guid("644B8D91-0D9D-421a-8070-7002FD503842");

		// Token: 0x04000A3B RID: 2619
		public static readonly Guid RgDataCenterManagementWkGuid = new Guid("A60E029A-FAF6-46dc-9FB6-0383FF786F36");

		// Token: 0x04000A3C RID: 2620
		public static readonly Guid RgViewOnlyLocalServerAccessWkGuid = new Guid("BC6291B7-E8D3-44b6-B010-1A42C340B20A");

		// Token: 0x04000A3D RID: 2621
		public static readonly Guid RgDestructiveAccessWkGuid = new Guid("4A96E860-01A2-4314-B2E2-D537B54F11C1");

		// Token: 0x04000A3E RID: 2622
		public static readonly Guid RgElevatedPermissionsWkGuid = new Guid("8D08A813-05C4-4fcd-8081-BACEF3F9FD6F");

		// Token: 0x04000A3F RID: 2623
		public static readonly Guid RgViewOnlyWkGuid = new Guid("C4D9726F-9CCF-47c6-9B1E-2E4971FF6486");

		// Token: 0x04000A40 RID: 2624
		public static readonly Guid RgOperationsWkGuid = new Guid("1DC6B9B0-3580-44c3-AD04-C89172C40958");

		// Token: 0x04000A41 RID: 2625
		public static readonly Guid RgServiceAccountsWkGuid = new Guid("69910741-AF5A-4f25-904F-AE00A6C7582A");

		// Token: 0x04000A42 RID: 2626
		public static readonly Guid RgComplianceManagementWkGuid = new Guid("9B440AB3-B4A9-4520-8C4B-B22F33C52766");

		// Token: 0x04000A43 RID: 2627
		public static readonly Guid RgViewOnlyPIIWkGuid = new Guid("AAA4C161-1F7B-455B-820C-5D2EDB1A7D47");

		// Token: 0x04000A44 RID: 2628
		public static readonly Guid RgCapacityDestructiveAccessWkGuid = new Guid("7E3685CD-8BEF-4F41-AC45-0D0B14EDD008");

		// Token: 0x04000A45 RID: 2629
		public static readonly Guid RgCapacityServerAdminsWkGuid = new Guid("370a9745-22bd-4e9c-80fa-162363e48148");

		// Token: 0x04000A46 RID: 2630
		public static readonly Guid RgCustomerChangeAccessWkGuid = new Guid("1649a084-f94c-4393-9cce-4530a491d1fd");

		// Token: 0x04000A47 RID: 2631
		public static readonly Guid RgCustomerDataAccessWkGuid = new Guid("b83f41a6-cd6e-41d0-bd13-284361b48ce9");

		// Token: 0x04000A48 RID: 2632
		public static readonly Guid RgAccessToCustomerDataDCOnlyWkGuid = new Guid("4CE08A5A-224F-416E-B77C-B34A7AE56E6B");

		// Token: 0x04000A49 RID: 2633
		public static readonly Guid RgDatacenterOperationsDCOnlyWkGuid = new Guid("BD34316F-FB28-433A-A218-52816234E5F3");

		// Token: 0x04000A4A RID: 2634
		public static readonly Guid RgCustomerDestructiveAccessWkGuid = new Guid("93889eb0-ee19-4229-b5a8-ff0732a3b07a");

		// Token: 0x04000A4B RID: 2635
		public static readonly Guid RgCustomerPIIAccessWkGuid = new Guid("e6abdf42-cdac-4c7b-97ad-4b3b6466796b");

		// Token: 0x04000A4C RID: 2636
		public static readonly Guid RgManagementAdminAccessWkGuid = new Guid("ac2562f4-866e-4049-8ae8-42649ec73917");

		// Token: 0x04000A4D RID: 2637
		public static readonly Guid RgManagementCACoreAdminWkGuid = new Guid("5bf65a76-317f-409f-a2d7-a0c1dc2b2569");

		// Token: 0x04000A4E RID: 2638
		public static readonly Guid RgManagementChangeAccessWkGuid = new Guid("a81ca1e9-d1e5-4cf3-a69c-4d89c63deb22");

		// Token: 0x04000A4F RID: 2639
		public static readonly Guid RgManagementDestructiveAccessWkGuid = new Guid("fe45aa5a-b32e-45d3-837c-dd0b00cc4df2");

		// Token: 0x04000A50 RID: 2640
		public static readonly Guid RgCapacityFrontendServerAdminsWkGuid = new Guid("3528e4d2-4faf-4c75-b374-50fc3bfbb8fd");

		// Token: 0x04000A51 RID: 2641
		public static readonly Guid RgManagementServerAdminsWkGuid = new Guid("aee383e0-711c-4625-851b-4a4c0d0c117e");

		// Token: 0x04000A52 RID: 2642
		public static readonly Guid RgCapacityDCAdminsWkGuid = new Guid("d7e9161c-495e-4c2f-91ba-e044bda511db");

		// Token: 0x04000A53 RID: 2643
		public static readonly Guid RgNetworkingAdminAccessWkGuid = new Guid("6bacb10c-31fe-4837-903a-f5d227cf95ec");

		// Token: 0x04000A54 RID: 2644
		public static readonly Guid RgNetworkingChangeAccessWkGuid = new Guid("d5eb34ed-1a14-4e69-b454-e7bb867104e2");

		// Token: 0x04000A55 RID: 2645
		public static readonly Guid RgCommunicationManagersWkGuid = new Guid("f26700d2-55ca-4b3d-9779-002078475902");

		// Token: 0x04000A56 RID: 2646
		public static readonly Guid RgMailboxManagementWkGuid = new Guid("acf98416-bb5d-45d7-9a23-3bec7fe981f5");

		// Token: 0x04000A57 RID: 2647
		public static readonly Guid RgFfoAntiSpamAdminsWkGuid = new Guid("36A79B6E-0872-4D33-92C8-B813BBCD3C9A");

		// Token: 0x04000A58 RID: 2648
		public static readonly Guid RgDedicatedSupportAccessWkGuid = new Guid("12abd11b-d764-44d3-b862-9c5ab6e90088");

		// Token: 0x04000A59 RID: 2649
		public static readonly Guid RgAppLockerExemptionWkGuid = new Guid("e24a1b87-6a64-46c4-b9c8-91f878f9d31c");

		// Token: 0x04000A5A RID: 2650
		public static readonly Guid RgECSAdminServerAccessWkGuid = new Guid("64092ddc-75d1-4a6d-980d-30e79e3cf251");

		// Token: 0x04000A5B RID: 2651
		public static readonly Guid RgECSPIIAccessServerAccessWkGuid = new Guid("164a26b9-2b54-4ce7-bb2a-d033897e7da8");

		// Token: 0x04000A5C RID: 2652
		public static readonly Guid RgECSAdminWkGuid = new Guid("c36ca20f-3d1d-40b5-a7a9-c598298e083d");

		// Token: 0x04000A5D RID: 2653
		public static readonly Guid RgECSPIIAccessWkGuid = new Guid("dbf37be1-038c-4535-b74e-7bbd2f453a7c");
	}
}
