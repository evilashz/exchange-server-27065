using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000015 RID: 21
	internal class DetailsTemplatesResultPane : CaptionedResultPane
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000507A File Offset: 0x0000327A
		public override string SelectionHelpTopic
		{
			get
			{
				return SelectionHelpTopics.DetailsTemplate;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005081 File Offset: 0x00003281
		static DetailsTemplatesResultPane()
		{
			DetailsTemplatesResultPane.iconLibrary.Icons.Add("DetailsTemplate", Icons.DetailsTemplate);
			DetailsTemplatesResultPane.iconLibrary.Icons.Add("RestoreDefaultTemplate", Icons.RestoreDefaultTemplate);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000050C0 File Offset: 0x000032C0
		public DetailsTemplatesResultPane() : base(ResultPaneProfileLoader.Loader, "DetailsTemplate")
		{
			base.Name = "DetailsTemplatesResultPane";
			base.Icon = Icons.DetailsTemplate;
			ObjectList objectList = new ObjectList();
			objectList.Name = "DetailsTemplatesResultPaneDataListView";
			objectList.Dock = DockStyle.Fill;
			objectList.ListView.IconLibrary = DetailsTemplatesResultPane.iconLibrary;
			objectList.ListView.ImageIndex = 0;
			objectList.FilterControl.ObjectSchema = ObjectSchema.GetInstance<DetailsTemplatesResultPane.DetailsTemplatesFilterSchema>();
			FilterablePropertyDescription filterablePropertyDescription = new FilterablePropertyDescription(DetailsTemplatesResultPane.DetailsTemplatesFilterSchema.TemplateType, Strings.TypeColumnName, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual
			});
			filterablePropertyDescription.FilterableListSource = new ObjectListSource(new string[]
			{
				"Contact",
				"Group",
				"Mailbox Agent",
				"Public Folder",
				"Search Dialog",
				"User"
			});
			objectList.FilterControl.PropertiesToFilter.Add(filterablePropertyDescription);
			objectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(DetailsTemplatesResultPane.DetailsTemplatesFilterSchema.Language, Strings.LanguageColumnName, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.Contains,
				PropertyFilterOperator.NotContains,
				PropertyFilterOperator.StartsWith,
				PropertyFilterOperator.EndsWith
			}));
			base.ListControl = objectList.ListView;
			base.FilterControl = objectList.FilterControl;
			base.Controls.Add(objectList);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005218 File Offset: 0x00003418
		protected override void SetupCommandsProfile()
		{
			base.SetupCommandsProfile();
			ResultsCommandProfile item = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "restoreDetailsTemplate",
					Icon = Icons.RestoreDefaultTemplate,
					Text = Strings.RestoreDetailsTemplate
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Update,
					IsSelectionCommand = true
				},
				Action = new ResultsTaskCommandAction
				{
					CommandText = "Restore-DetailsTemplate",
					SingleSelectionConfirmation = new SingleSelectionMessageDelegate(Strings.RestoreDetailsTemplateConfirmMessage),
					MultipleSelectionConfirmation = new MultipleSelectionMessageDelegate(Strings.RestoreDetailsTemplatesConfirmMessage)
				}
			};
			base.CommandsProfile.CustomSelectionCommands.Add(item);
			ResultsCommandProfile item2 = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "Properties",
					Icon = Icons.DetailsTemplate,
					Text = Strings.EditDetailsTemplate
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Update,
					IsSelectionCommand = true,
					RequiresSingleSelection = true,
					IsPropertiesCommand = true,
					UseSingleRowRefresh = true
				},
				Action = new ShowDetailsTemplatesProperitiesAction()
			};
			base.CommandsProfile.ShowSelectionPropertiesCommands.Add(item2);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000536F File Offset: 0x0000356F
		protected override ExchangeSettings CreatePrivateSettings(IComponent owner)
		{
			return new DetailsTemplatesEditorSettings(this);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005377 File Offset: 0x00003577
		protected override int MultiRowRefreshThreshold
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000042 RID: 66
		internal const string TypeColumnName = "TemplateType";

		// Token: 0x04000043 RID: 67
		internal const string LanguageColumnName = "Language";

		// Token: 0x04000044 RID: 68
		private static IconLibrary iconLibrary = new IconLibrary();

		// Token: 0x02000016 RID: 22
		public class DetailsTemplatesFilterSchema : ObjectSchema
		{
			// Token: 0x04000045 RID: 69
			public static readonly ADPropertyDefinition Language = new ADPropertyDefinition("Language", ExchangeObjectVersion.Exchange2003, typeof(string), "Language", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

			// Token: 0x04000046 RID: 70
			public static readonly ADPropertyDefinition TemplateType = new ADPropertyDefinition("TemplateType", ExchangeObjectVersion.Exchange2003, typeof(string), "TemplateType", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
		}
	}
}
