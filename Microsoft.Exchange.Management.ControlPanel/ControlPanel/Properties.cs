using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200006C RID: 108
	[PersistChildren(true)]
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(WebServiceMethod))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:Properties runat=server></{0}:Properties>")]
	public class Properties : DataBoundControl, IBaseFormContentControl, IScriptControl
	{
		// Token: 0x06001AAA RID: 6826 RVA: 0x00054E48 File Offset: 0x00053048
		public Properties()
		{
			this.ID = "Properties";
			this.UseSetObject = true;
			this.HasSaveMethod = true;
			this.SkipReadOnlyCheck = false;
			this.Sections = new SectionCollection(this);
			this.ExceptionHandlers = new List<WebServiceExceptionHandler>();
			this.CaptionTextField = "Identity.DisplayName";
			this.UrlRequiresId = true;
			this.Bindings = new DataContractBinding();
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00054EB8 File Offset: 0x000530B8
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00054F1F File Offset: 0x0005311F
		public Identity ObjectIdentity
		{
			get
			{
				if (this.objectIdentity == null)
				{
					string name = string.IsNullOrEmpty(this.IdQueryStringField) ? "id" : this.IdQueryStringField;
					string text = this.Context.Request.QueryString[name];
					if (!string.IsNullOrEmpty(text))
					{
						this.objectIdentity = Identity.ParseIdentity(text);
					}
				}
				return this.objectIdentity;
			}
			protected set
			{
				this.objectIdentity = value;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00054F28 File Offset: 0x00053128
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x00054F30 File Offset: 0x00053130
		public string IdQueryStringField { get; set; }

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00054F39 File Offset: 0x00053139
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x00054F41 File Offset: 0x00053141
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00054F4A File Offset: 0x0005314A
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x00054F52 File Offset: 0x00053152
		public WebServiceMethod RefreshWebServiceMethod { get; private set; }

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00054F5B File Offset: 0x0005315B
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00054F63 File Offset: 0x00053163
		public WebServiceMethod SaveWebServiceMethod { get; private set; }

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00054F6C File Offset: 0x0005316C
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x00054F74 File Offset: 0x00053174
		public bool UseSetObject { get; set; }

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x00054F7D File Offset: 0x0005317D
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x00054F85 File Offset: 0x00053185
		public bool? AlwaysInvokeSave { get; set; }

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00054F8E File Offset: 0x0005318E
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x00054F96 File Offset: 0x00053196
		public bool GetObjectForNew { get; set; }

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00054F9F File Offset: 0x0005319F
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x00054FA7 File Offset: 0x000531A7
		public bool HasSaveMethod { get; set; }

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00054FB0 File Offset: 0x000531B0
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x00054FB8 File Offset: 0x000531B8
		public string SaveMethod { get; set; }

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00054FC1 File Offset: 0x000531C1
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00054FC9 File Offset: 0x000531C9
		[DefaultValue(false)]
		public bool SkipReadOnlyCheck { get; set; }

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x00054FD2 File Offset: 0x000531D2
		public bool ReadOnly
		{
			get
			{
				return !this.SkipReadOnlyCheck && this.allControlsDisabled;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00054FE4 File Offset: 0x000531E4
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00054FEC File Offset: 0x000531EC
		public string SaveMethodExpression { get; set; }

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00054FF5 File Offset: 0x000531F5
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00054FFD File Offset: 0x000531FD
		public WebServiceParameterNames ParameterSet { get; set; }

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00055006 File Offset: 0x00053206
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0005500E File Offset: 0x0005320E
		public string OnRefreshSucceed { get; set; }

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00055017 File Offset: 0x00053217
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x0005501F File Offset: 0x0005321F
		public bool UrlRequiresId { get; set; }

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00055028 File Offset: 0x00053228
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00055030 File Offset: 0x00053230
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SectionCollection Sections { get; private set; }

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00055039 File Offset: 0x00053239
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00055041 File Offset: 0x00053241
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<WebServiceExceptionHandler> ExceptionHandlers { get; private set; }

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0005504A File Offset: 0x0005324A
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00055052 File Offset: 0x00053252
		[TemplateContainer(typeof(PropertiesContentPanel))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[Description("Property Pane Content")]
		[TemplateInstance(TemplateInstance.Single)]
		public virtual ITemplate Content { get; set; }

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0005505B File Offset: 0x0005325B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00055069 File Offset: 0x00053269
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00055071 File Offset: 0x00053271
		[DefaultValue("Identity.DisplayName")]
		public string CaptionTextField { get; set; }

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0005507A File Offset: 0x0005327A
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00055082 File Offset: 0x00053282
		public string NameProperty { get; set; }

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0005508B File Offset: 0x0005328B
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x00055093 File Offset: 0x00053293
		public bool UseWarningPanel { get; set; }

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0005509C File Offset: 0x0005329C
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x000550A4 File Offset: 0x000532A4
		public bool SuppressWarning { get; set; }

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x000550AD File Offset: 0x000532AD
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x000550B5 File Offset: 0x000532B5
		public bool HideClientValidationError { get; set; }

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000550BE File Offset: 0x000532BE
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x000550C6 File Offset: 0x000532C6
		public string SaveConfirmationText { get; set; }

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x000550CF File Offset: 0x000532CF
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x000550D7 File Offset: 0x000532D7
		protected PowerShellResults Results { get; set; }

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000550E0 File Offset: 0x000532E0
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x000550E8 File Offset: 0x000532E8
		protected DataContractBinding Bindings { get; set; }

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000550F1 File Offset: 0x000532F1
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x000550F5 File Offset: 0x000532F5
		private bool IsResultBindingAllowed
		{
			get
			{
				return this.RefreshWebServiceMethod != null || (this.Page is BaseForm && (this.Page as BaseForm).PassingDataOnClient);
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00055120 File Offset: 0x00053320
		private bool RequireDataAtInitialize
		{
			get
			{
				return this.UseSetObject || this.GetObjectForNew;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00055132 File Offset: 0x00053332
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0005513A File Offset: 0x0005333A
		public PropertiesContentPanel ContentContainer { get; private set; }

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00055143 File Offset: 0x00053343
		public void AddBinding(string contractPropertyName, Control targetControl, string clientPropertyName)
		{
			this.Bindings.Bindings.Add(contractPropertyName, new ClientControlBinding(targetControl, clientPropertyName));
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00055160 File Offset: 0x00053360
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return new ScriptControlDescriptor[]
			{
				this.GetScriptDescriptor()
			};
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0005517E File Offset: 0x0005337E
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0005518C File Offset: 0x0005338C
		internal static void ApplyRolesFilterRecursive(Control c, IPrincipal currentUser, IVersionable versionableObject)
		{
			IAttributeAccessor attributeAccessor = c as IAttributeAccessor;
			if (attributeAccessor != null)
			{
				string attribute = attributeAccessor.GetAttribute("SetRoles");
				string attribute2 = attributeAccessor.GetAttribute("DataBoundProperty");
				PropertyDefinition propertyDefinition = (versionableObject != null && !string.IsNullOrEmpty(attribute2)) ? versionableObject.ObjectSchema[attribute2] : null;
				if (propertyDefinition != null && !versionableObject.IsPropertyAccessible(propertyDefinition))
				{
					Properties.HideControl(c, Properties.FindAssociatedLabel(c));
				}
				else if ((!string.IsNullOrEmpty(attribute) && !LoginUtil.IsInRoles(currentUser, attribute.Split(new char[]
				{
					','
				}))) || (!string.IsNullOrEmpty(attribute2) && versionableObject != null && versionableObject.IsReadOnly))
				{
					string attribute3 = attributeAccessor.GetAttribute("NoRoleState");
					Label associatedLabel = Properties.FindAssociatedLabel(c);
					if (!string.IsNullOrEmpty(attribute3) && NoRoleState.Hide == (NoRoleState)Enum.Parse(typeof(NoRoleState), attribute3))
					{
						Properties.HideControl(c, associatedLabel);
					}
					else
					{
						Properties.MakeControlRbacDisabled(c, associatedLabel);
						if (!string.IsNullOrEmpty(attributeAccessor.GetAttribute("helpId")))
						{
							attributeAccessor.SetAttribute("helpId", string.Empty);
						}
						attributeAccessor.SetAttribute("MandatoryParam", null);
					}
				}
			}
			if (c.HasControls())
			{
				foreach (object obj in c.Controls)
				{
					Control c2 = (Control)obj;
					Properties.ApplyRolesFilterRecursive(c2, currentUser, versionableObject);
				}
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00055308 File Offset: 0x00053508
		internal static Label FindAssociatedLabel(Control control)
		{
			return control.NamingContainer.FindControl(control.ID + "_label") as Label;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0005532A File Offset: 0x0005352A
		internal static void HideControl(Control c, Label associatedLabel)
		{
			c.Visible = false;
			if (associatedLabel != null)
			{
				associatedLabel.Visible = false;
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0005533D File Offset: 0x0005353D
		internal static void MakeControlRbacDisabled(Control c, Label associatedLabel)
		{
			Util.MakeControlRbacDisabled(c);
			if (associatedLabel != null)
			{
				associatedLabel.Enabled = false;
				Util.MarkRBACDisabled(associatedLabel);
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00055355 File Offset: 0x00053555
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00055364 File Offset: 0x00053564
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				this.CssClass = "propPane";
			}
			else
			{
				this.CssClass += " propPane";
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0005539D File Offset: 0x0005359D
		protected override void Render(HtmlTextWriter writer)
		{
			this.RemoveMetaAttributes(this);
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000553C8 File Offset: 0x000535C8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.warningPanel = this.CreateWarningPanel("PropertyPaneWarningPanel");
			this.Controls.Add(this.warningPanel);
			if (this.Content != null && this.Sections.Count > 0)
			{
				throw new NotSupportedException("Properties control cannot have both sections and content");
			}
			if (this.Content != null)
			{
				this.ContentContainer = new PropertiesContentPanel();
				this.ContentContainer.ID = "contentContainer";
				this.Controls.Add(this.ContentContainer);
				this.Content.InstantiateIn(this.ContentContainer);
			}
			if (this.ServiceUrl != null)
			{
				if (this.HasSaveMethod)
				{
					if (!this.UseSetObject && (!string.IsNullOrEmpty(this.SaveMethod) || !string.IsNullOrEmpty(this.SaveMethodExpression)))
					{
						throw new NotSupportedException("Not supported to set \"UseSetObject\" to false and then use the properties \"SaveMethod\" or \"SaveMethodExpression\" on properties control");
					}
					this.SaveWebServiceMethod = new WebServiceMethod();
					this.SaveWebServiceMethod.ID = "Save";
					this.SaveWebServiceMethod.ServiceUrl = this.ServiceUrl;
					this.SaveWebServiceMethod.AlwaysInvokeSave = (this.AlwaysInvokeSave ?? (!this.UseSetObject));
					if (string.IsNullOrEmpty(this.SaveMethodExpression) && !string.IsNullOrEmpty(this.SaveMethod))
					{
						this.SaveWebServiceMethod.Method = this.SaveMethod;
					}
					else
					{
						this.SaveWebServiceMethod.Method = (this.UseSetObject ? "SetObject" : "NewObject");
					}
					if (this.ParameterSet != WebServiceParameterNames.NONE)
					{
						this.SaveWebServiceMethod.ParameterNames = this.ParameterSet;
					}
					else
					{
						this.SaveWebServiceMethod.ParameterNames = (this.UseSetObject ? WebServiceParameterNames.SetObject : WebServiceParameterNames.NewObject);
					}
					IPrincipal user = this.Context.User;
					foreach (WebServiceExceptionHandler webServiceExceptionHandler in this.ExceptionHandlers)
					{
						if (webServiceExceptionHandler.ApplyRbacRolesAndAddControls(this, user))
						{
							this.SaveWebServiceMethod.ExceptionHandlers.Add(webServiceExceptionHandler);
							this.RemoveMetaAttributes(webServiceExceptionHandler);
						}
					}
					this.Controls.Add(this.SaveWebServiceMethod);
				}
				if (this.UseSetObject)
				{
					this.RefreshWebServiceMethod = new WebServiceMethod();
					this.RefreshWebServiceMethod.ID = "Refresh";
					this.RefreshWebServiceMethod.ServiceUrl = this.ServiceUrl;
					this.RefreshWebServiceMethod.Method = "GetObject";
					this.RefreshWebServiceMethod.ParameterNames = WebServiceParameterNames.GetObject;
					this.Controls.Add(this.RefreshWebServiceMethod);
				}
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0005565C File Offset: 0x0005385C
		protected override void OnPreRender(EventArgs e)
		{
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<Properties>(this);
			if (this.ServiceUrl != null && this.RequireDataAtInitialize)
			{
				if (this.UrlRequiresId && null == this.ObjectIdentity)
				{
					throw new BadQueryParameterException("id");
				}
				if (this.UseSetObject)
				{
					this.Results = this.ServiceUrl.GetObject(this.ObjectIdentity);
					this.Results.UseAsRbacScopeInCurrentHttpContext();
					this.configObject = (RbacQuery.LegacyTargetObject as IVersionable);
					if (this.configObject != null && this.configObject.IsReadOnly)
					{
						string[] array = new string[]
						{
							Strings.VersionMismatchWarning(this.configObject.ExchangeVersion.ExchangeBuild)
						};
						this.Results.Warnings = (this.Results.Warnings.IsNullOrEmpty() ? array : this.Results.Warnings.Concat(array).ToArray<string>());
					}
				}
				else if (this.GetObjectForNew)
				{
					this.Results = this.ServiceUrl.GetObjectForNew(this.ObjectIdentity);
				}
			}
			this.Page.PreRenderComplete += this.OnPreRenderComplete;
			base.OnPreRender(e);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000557A1 File Offset: 0x000539A1
		protected void OnPreRenderComplete(object sender, EventArgs e)
		{
			Properties.ApplyRolesFilterRecursive(this, this.Context.User, this.configObject);
			this.CreateClientBindings();
			this.HideSectionWithoutAnyDataBinding();
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000557C8 File Offset: 0x000539C8
		protected WebControl GetCaptionLabel()
		{
			WebControl result = null;
			BaseForm baseForm = this.Page as BaseForm;
			if (baseForm != null)
			{
				result = baseForm.CaptionLabel;
			}
			else
			{
				for (Control parent = this.Parent; parent != null; parent = parent.Parent)
				{
					SlabFrame slabFrame = parent as SlabFrame;
					if (slabFrame != null)
					{
						result = slabFrame.CaptionLabel;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00055818 File Offset: 0x00053A18
		protected virtual ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("PropertyPage", this.ClientID);
			scriptControlDescriptor.AddProperty("UseSetObject", this.UseSetObject, true);
			scriptControlDescriptor.AddProperty("RequireDataAtInitialize", this.RequireDataAtInitialize, true);
			BaseForm baseForm = this.Page as BaseForm;
			if (baseForm != null)
			{
				scriptControlDescriptor.AddComponentProperty("Form", "aspnetForm", true);
			}
			scriptControlDescriptor.AddClientIdProperty("ContentContainerID", this.ContentContainer);
			if (null != this.ObjectIdentity)
			{
				scriptControlDescriptor.AddScriptProperty("ObjectIdentity", this.ObjectIdentity.ToJsonString(null));
			}
			if (this.Results != null)
			{
				scriptControlDescriptor.AddProperty("JsonResults", this.Results.ToJsonString(DDIService.KnownTypes.Value));
				if (!string.IsNullOrEmpty(this.SaveMethodExpression))
				{
					scriptControlDescriptor.AddScriptProperty("SaveMethodExpression", "function($_){ return " + this.SaveMethodExpression + "}");
				}
			}
			if (this.Bindings != null)
			{
				scriptControlDescriptor.AddScriptProperty("Bindings", this.Bindings.ToJavaScript(null));
				WebControl captionLabel = this.GetCaptionLabel();
				if (captionLabel != null && !string.IsNullOrEmpty(this.CaptionTextField))
				{
					scriptControlDescriptor.AddScriptProperty("CaptionTextField", "function($_){ return $_." + this.CaptionTextField + "; }");
					scriptControlDescriptor.AddElementProperty("CaptionControl", captionLabel.ClientID);
				}
			}
			scriptControlDescriptor.AddComponentProperty("RefreshWebServiceMethod", this.RefreshWebServiceMethod);
			if (!this.ReadOnly)
			{
				scriptControlDescriptor.AddComponentProperty("SaveWebServiceMethod", this.SaveWebServiceMethod);
			}
			scriptControlDescriptor.AddProperty("UseWarningPanel", this.UseWarningPanel, true);
			scriptControlDescriptor.AddProperty("SuppressWarning", this.SuppressWarning, true);
			scriptControlDescriptor.AddClientIdProperty("WarningPanelID", this.warningPanel);
			scriptControlDescriptor.AddProperty("HideClientValidationError", this.HideClientValidationError, true);
			scriptControlDescriptor.AddProperty("SaveConfirmationText", this.SaveConfirmationText, true);
			if (!string.IsNullOrEmpty(this.OnRefreshSucceed))
			{
				scriptControlDescriptor.AddScriptProperty("OnRefreshSucceed", this.OnRefreshSucceed);
			}
			if (this.NameProperty != "Name")
			{
				scriptControlDescriptor.AddProperty("NameProperty", this.NameProperty);
			}
			return scriptControlDescriptor;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00055A34 File Offset: 0x00053C34
		private static bool IsReadOnly(WebControl c)
		{
			if (c is TextBox)
			{
				return ((TextBox)c).ReadOnly;
			}
			if (c is EcpCollectionEditor)
			{
				return ((EcpCollectionEditor)c).ReadOnly;
			}
			return c is WebServiceListSource || c is EllipsisLabel || !c.Enabled;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00055A84 File Offset: 0x00053C84
		private void RemoveMetaAttributes(Control c)
		{
			WebControl webControl = c as WebControl;
			if (webControl != null && webControl.HasAttributes)
			{
				webControl.Attributes.Remove("DataBoundProperty");
				webControl.Attributes.Remove("BoundControlProperty");
				webControl.Attributes.Remove("ClientPropertyName");
				webControl.Attributes.Remove("SetRoles");
				webControl.Attributes.Remove("NoRoleState");
				webControl.Attributes.Remove("EncodeHtml");
				webControl.Attributes.Remove("MandatoryParam");
				webControl.Attributes.Remove("SortedDirection");
			}
			if (c.HasControls())
			{
				foreach (object obj in c.Controls)
				{
					Control c2 = (Control)obj;
					this.RemoveMetaAttributes(c2);
				}
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00055B80 File Offset: 0x00053D80
		private bool HasAssociatedChildControls(RadioButtonList rbl)
		{
			if (rbl.Items.Count == 2 && ((rbl.Items[0].Value == "true" && rbl.Items[1].Value == "false") || (rbl.Items[0].Value == "false" && rbl.Items[1].Value == "true")))
			{
				foreach (object obj in rbl.NamingContainer.Controls)
				{
					Control control = (Control)obj;
					if (control is ControlAssociationExtender && ((ControlAssociationExtender)control).TargetControlID == rbl.ID)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00055F50 File Offset: 0x00054150
		private IEnumerable<WebControl> GetVisibleClientBoundControls(Control parent)
		{
			if (parent.Visible)
			{
				WebControl wc = parent as WebControl;
				if (wc != null)
				{
					string dataProperty = wc.Attributes["DataBoundProperty"];
					if (!string.IsNullOrEmpty(dataProperty))
					{
						yield return wc;
					}
				}
				foreach (object obj in parent.Controls)
				{
					Control subControl = (Control)obj;
					foreach (WebControl c in this.GetVisibleClientBoundControls(subControl))
					{
						yield return c;
					}
				}
			}
			yield break;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00055F74 File Offset: 0x00054174
		private void HideSectionWithoutAnyDataBinding()
		{
			foreach (Section section in this.Sections)
			{
				IEnumerator<WebControl> enumerator2 = this.GetVisibleClientBoundControls(section).GetEnumerator();
				if (!enumerator2.MoveNext())
				{
					section.Visible = false;
				}
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00055FD8 File Offset: 0x000541D8
		private void CreateClientBindings()
		{
			foreach (WebControl webControl in this.GetVisibleClientBoundControls(this))
			{
				string key = webControl.Attributes["DataBoundProperty"];
				string text = webControl.Attributes["ClientPropertyName"];
				string text2 = webControl.Attributes["MandatoryParam"];
				if (text2 != null && text2 != "false" && text2 != "true")
				{
					throw new NotSupportedException("MandatoryParam attribute value can either be 'true' or 'false'.");
				}
				bool flag = text2 != null && Convert.ToBoolean(text2);
				Binding binding;
				if (webControl is Label && (text == null || text == "innerHTML"))
				{
					string text3 = webControl.Attributes["EncodeHtml"];
					if (text3 != null && text3 != "false" && text3 != "true")
					{
						throw new NotSupportedException("EncodeHtml attribute value can either be 'true' or 'false'.");
					}
					bool flag2 = text3 == null || Convert.ToBoolean(text3);
					if (flag2)
					{
						binding = new LabelBinding(webControl);
					}
					else
					{
						binding = new NonEncodedLabelBinding(webControl);
					}
				}
				else
				{
					if (text == null)
					{
						if (webControl is CheckBox && !(webControl is RadioButton))
						{
							text = "checked";
						}
						else
						{
							text = "value";
						}
					}
					if (webControl is IScriptControl)
					{
						if (webControl is AjaxUploader)
						{
							binding = new AjaxUploaderBinding(webControl, text);
						}
						else
						{
							binding = new ComponentBinding(webControl, text);
						}
					}
					else if (webControl is DownloadedImage)
					{
						DownloadedImage downloadedImage = (DownloadedImage)webControl;
						binding = new ImageUrlBinding(downloadedImage, downloadedImage.ReadOnly);
					}
					else if (webControl is DropDownList)
					{
						string text4 = webControl.Attributes["SortedDirection"];
						if (text4 != null && text4 != SortDirection.Ascending.ToString() && text4 != SortDirection.Descending.ToString())
						{
							throw new NotSupportedException("SortedDirection attribute value can either be 'Ascending' or 'Descending'.");
						}
						if (text4 == null)
						{
							binding = new ComboBoxBinding(webControl, text);
						}
						else
						{
							binding = new SortedComboBoxBinding(webControl, text, (SortDirection)Enum.Parse(typeof(SortDirection), text4));
						}
					}
					else if (webControl is RadioButton || webControl is RadioButtonList)
					{
						binding = new RadioButtonBinding(webControl, text);
					}
					else if (text == "value")
					{
						binding = new ValueBinding(webControl);
					}
					else
					{
						binding = new ClientControlBinding(webControl, text);
					}
					if (flag || webControl is Label)
					{
						binding = new MandatoryBinding(binding);
					}
					else if (webControl is RadioButtonList && this.HasAssociatedChildControls((RadioButtonList)webControl))
					{
						binding = new MandatoryBinding(binding);
					}
					else if (Properties.IsReadOnly(webControl))
					{
						binding = new NeverDirtyBinding(binding);
					}
				}
				if (this.UseSetObject || !(binding is NeverDirtyBinding))
				{
					this.Bindings.Bindings.Add(key, binding);
					if (this.allControlsDisabled && !(webControl is Label) && !(webControl is EllipsisLabel) && !(webControl is CollectionViewer) && !Properties.IsReadOnly(webControl))
					{
						this.allControlsDisabled = false;
					}
				}
			}
		}

		// Token: 0x04001B21 RID: 6945
		internal const string DataBoundProperty = "DataBoundProperty";

		// Token: 0x04001B22 RID: 6946
		internal const string BoundControlProperty = "BoundControlProperty";

		// Token: 0x04001B23 RID: 6947
		internal const string SetRoles = "SetRoles";

		// Token: 0x04001B24 RID: 6948
		internal const string NoRoleStateStr = "NoRoleState";

		// Token: 0x04001B25 RID: 6949
		internal const string EncodeHtml = "EncodeHtml";

		// Token: 0x04001B26 RID: 6950
		internal const string MandatoryParam = "MandatoryParam";

		// Token: 0x04001B27 RID: 6951
		internal const string SortedDirection = "SortedDirection";

		// Token: 0x04001B28 RID: 6952
		internal const string StrHelpId = "helpId";

		// Token: 0x04001B29 RID: 6953
		private const string ClientPropertyName = "ClientPropertyName";

		// Token: 0x04001B2A RID: 6954
		private Panel warningPanel;

		// Token: 0x04001B2B RID: 6955
		private bool allControlsDisabled = true;

		// Token: 0x04001B2C RID: 6956
		private IVersionable configObject;

		// Token: 0x04001B2D RID: 6957
		private Identity objectIdentity;
	}
}
