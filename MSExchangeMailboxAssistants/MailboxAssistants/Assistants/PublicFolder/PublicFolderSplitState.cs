using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage.PublicFolder;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200017E RID: 382
	[KnownType(typeof(PublicFolderSplitPlan))]
	[KnownType(typeof(SplitOperationState))]
	[DataContract(IsReference = true)]
	[Serializable]
	internal sealed class PublicFolderSplitState : IPublicFolderSplitState, IExtensibleDataObject
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x0005B490 File Offset: 0x00059690
		public PublicFolderSplitState()
		{
			this.VersionNumber = SplitStateAdapter.CurrentVersion;
			this.IdentifyTargetMailboxState = new SplitOperationState();
			this.PrepareTargetMailboxState = new SplitOperationState();
			this.PrepareSplitPlanState = new SplitOperationState();
			this.MoveContentState = new SplitOperationState();
			this.OverallSplitState = new SplitOperationState();
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0005B4E5 File Offset: 0x000596E5
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0005B4ED File Offset: 0x000596ED
		[DataMember]
		public Version VersionNumber { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0005B4F6 File Offset: 0x000596F6
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0005B4FE File Offset: 0x000596FE
		[DataMember]
		public SplitProgressState ProgressState { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0005B507 File Offset: 0x00059707
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0005B50F File Offset: 0x0005970F
		[DataMember]
		public Guid TargetMailboxGuid { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0005B518 File Offset: 0x00059718
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x0005B520 File Offset: 0x00059720
		[DataMember]
		public IPublicFolderSplitPlan SplitPlan { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0005B529 File Offset: 0x00059729
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x0005B531 File Offset: 0x00059731
		[DataMember]
		public string PublicFolderMoveRequestName { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0005B53A File Offset: 0x0005973A
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0005B542 File Offset: 0x00059742
		[DataMember]
		public ISplitOperationState IdentifyTargetMailboxState { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0005B54B File Offset: 0x0005974B
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0005B553 File Offset: 0x00059753
		[DataMember]
		public ISplitOperationState PrepareTargetMailboxState { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0005B55C File Offset: 0x0005975C
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0005B564 File Offset: 0x00059764
		[DataMember]
		public ISplitOperationState PrepareSplitPlanState { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0005B56D File Offset: 0x0005976D
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0005B575 File Offset: 0x00059775
		[DataMember]
		public ISplitOperationState MoveContentState { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0005B57E File Offset: 0x0005977E
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0005B586 File Offset: 0x00059786
		[DataMember]
		public ISplitOperationState OverallSplitState { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0005B58F File Offset: 0x0005978F
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0005B597 File Offset: 0x00059797
		[DataMember]
		public IPublicFolderSplitState PreviousSplitJobState { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0005B5A0 File Offset: 0x000597A0
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0005B5A8 File Offset: 0x000597A8
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x06000F5A RID: 3930 RVA: 0x0005B5B4 File Offset: 0x000597B4
		public override string ToString()
		{
			string result = string.Empty;
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				OmitXmlDeclaration = true,
				NamespaceHandling = NamespaceHandling.OmitDuplicates,
				CheckCharacters = false
			};
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
				{
					SplitStateAdapter.Serializer.WriteObject(xmlWriter, this);
					xmlWriter.Flush();
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0005B660 File Offset: 0x00059860
		public XElement ToXElement()
		{
			string text = string.Empty;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
				{
					SplitStateAdapter.Serializer.WriteObject(xmlWriter, this);
					xmlWriter.Flush();
					text = stringWriter.ToString();
				}
			}
			XDocument xdocument = XDocument.Parse(text);
			XElement xelement = new XElement("SplitState");
			xelement.Add(xdocument.Nodes());
			return xelement;
		}
	}
}
