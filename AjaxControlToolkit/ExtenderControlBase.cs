using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000010 RID: 16
	[PersistChildren(false)]
	[Themeable(true)]
	[ParseChildren(true)]
	public abstract class ExtenderControlBase : ExtenderControl, IControlResolver
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600004F RID: 79 RVA: 0x00002B44 File Offset: 0x00000D44
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00002B7C File Offset: 0x00000D7C
		public event ResolveControlEventHandler ResolveControlID;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002BB1 File Offset: 0x00000DB1
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002BB9 File Offset: 0x00000DB9
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BC2 File Offset: 0x00000DC2
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002BDA File Offset: 0x00000DDA
		public bool Enabled
		{
			get
			{
				return !this.isDisposed && this.GetPropertyValue<bool>("Enabled", true);
			}
			set
			{
				this.SetPropertyValue<bool>("Enabled", value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002BE8 File Offset: 0x00000DE8
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public string ScriptPath
		{
			get
			{
				return this.GetPropertyValue<string>("ScriptPath", null);
			}
			set
			{
				if (!this.AllowScriptPath)
				{
					throw new InvalidOperationException("This class does not allow setting of ScriptPath.");
				}
				this.SetPropertyValue<string>("ScriptPath", value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C18 File Offset: 0x00000E18
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002C46 File Offset: 0x00000E46
		public string BehaviorID
		{
			get
			{
				string propertyValue = this.GetPropertyValue<string>("BehaviorID", string.Empty);
				if (!string.IsNullOrEmpty(propertyValue))
				{
					return propertyValue;
				}
				return this.ClientID;
			}
			set
			{
				this.SetPropertyValue<string>("BehaviorID", value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002C54 File Offset: 0x00000E54
		[Obsolete("WARNING: ProfileBindings are disabled for this Toolkit release pending technical issues.  We hope to re-enable this in an upcoming release")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ProfilePropertyBindingCollection ProfileBindings
		{
			get
			{
				if (this.profileBindings == null)
				{
					this.profileBindings = new ProfilePropertyBindingCollection();
				}
				return this.profileBindings;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002C6F File Offset: 0x00000E6F
		protected virtual bool AllowScriptPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C74 File Offset: 0x00000E74
		protected virtual string ClientControlType
		{
			get
			{
				ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
				return clientScriptResourceAttribute.ComponentType;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002CA2 File Offset: 0x00000EA2
		protected Control TargetControl
		{
			get
			{
				return this.FindControlHelper(base.TargetControlID);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public override void Dispose()
		{
			this.isDisposed = true;
			base.Dispose();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CBF File Offset: 0x00000EBF
		public override Control FindControl(string id)
		{
			return this.FindControlHelper(id);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public virtual void EnsureValid()
		{
			this.CheckIfValid(true);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public Control ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002CDC File Offset: 0x00000EDC
		internal IEnumerable<ScriptReference> EnsureScripts()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(ScriptObjectBuilder.GetScriptReferences(base.GetType(), null != this.ScriptPath));
			string scriptPath = this.ScriptPath;
			if (!string.IsNullOrEmpty(scriptPath))
			{
				list.Add(new ScriptReference(scriptPath));
			}
			return list;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D28 File Offset: 0x00000F28
		protected static void SuppressUnusedParameterWarning(object unused)
		{
			if (unused != null)
			{
				unused.GetType();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D34 File Offset: 0x00000F34
		protected Control FindControlHelper(string id)
		{
			Control control;
			if (this.findControlHelperCache.ContainsKey(id))
			{
				control = this.findControlHelperCache[id];
			}
			else
			{
				control = base.FindControl(id);
				Control namingContainer = this.NamingContainer;
				while (control == null && namingContainer != null)
				{
					control = namingContainer.FindControl(id);
					namingContainer = namingContainer.NamingContainer;
				}
				if (control == null)
				{
					ResolveControlEventArgs resolveControlEventArgs = new ResolveControlEventArgs(id);
					this.OnResolveControlID(resolveControlEventArgs);
					control = resolveControlEventArgs.Control;
				}
				if (control != null)
				{
					this.findControlHelperCache[id] = control;
				}
			}
			return control;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002DB0 File Offset: 0x00000FB0
		protected string GetClientID(string controlId)
		{
			Control control = this.FindControlHelper(controlId);
			if (control != null)
			{
				controlId = control.ClientID;
			}
			return controlId;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DD1 File Offset: 0x00000FD1
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DEE File Offset: 0x00000FEE
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DFD File Offset: 0x00000FFD
		protected virtual void OnResolveControlID(ResolveControlEventArgs e)
		{
			if (this.ResolveControlID != null)
			{
				this.ResolveControlID(this, e);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E14 File Offset: 0x00001014
		protected virtual void RenderInnerScript(ScriptBehaviorDescriptor descriptor)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E18 File Offset: 0x00001018
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl)
		{
			if (!this.Enabled || !this.TargetControl.Visible)
			{
				return null;
			}
			ScriptBehaviorDescriptor scriptBehaviorDescriptor = new ScriptBehaviorDescriptor(this.ClientControlType, targetControl.ClientID);
			this.BuildScriptDescriptor(scriptBehaviorDescriptor);
			this.RenderInnerScript(scriptBehaviorDescriptor);
			return new List<ScriptDescriptor>(new ScriptDescriptor[]
			{
				scriptBehaviorDescriptor
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E6D File Offset: 0x0000106D
		protected virtual void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("id", this.BehaviorID);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E80 File Offset: 0x00001080
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (this.Enabled)
			{
				return this.EnsureScripts();
			}
			return null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E94 File Offset: 0x00001094
		protected virtual bool CheckIfValid(bool throwException)
		{
			bool result = true;
			foreach (object obj in TypeDescriptor.GetProperties(this))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Attributes[typeof(RequiredPropertyAttribute)] != null && (propertyDescriptor.GetValue(this) == null || !propertyDescriptor.ShouldSerializeValue(this)))
				{
					result = false;
					if (throwException)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "{0} missing required {1} property value for {2}.", new object[]
						{
							base.GetType().ToString(),
							propertyDescriptor.Name,
							this.ID
						}), propertyDescriptor.Name);
					}
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F60 File Offset: 0x00001160
		protected V GetPropertyValue<V>(string propertyName, V nullValue)
		{
			if (this.ViewState[propertyName] == null)
			{
				return nullValue;
			}
			return (V)((object)this.ViewState[propertyName]);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F83 File Offset: 0x00001183
		protected void SetPropertyValue<V>(string propertyName, V value)
		{
			this.ViewState[propertyName] = value;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F97 File Offset: 0x00001197
		[Obsolete("Use GetPropertyValue<V> instead")]
		protected string GetPropertyStringValue(string propertyName)
		{
			return this.GetPropertyValue<string>(propertyName, string.Empty);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FA5 File Offset: 0x000011A5
		[Obsolete("Use SetPropertyValue<V> instead")]
		protected void SetPropertyStringValue(string propertyName, string value)
		{
			this.SetPropertyValue<string>(propertyName, value);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FAF File Offset: 0x000011AF
		[Obsolete("Use GetPropertyValue<V> instead")]
		protected int GetPropertyIntValue(string propertyName)
		{
			return this.GetPropertyValue<int>(propertyName, 0);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002FB9 File Offset: 0x000011B9
		[Obsolete("Use SetPropertyValue<V> instead")]
		protected void SetPropertyIntValue(string propertyName, int value)
		{
			this.SetPropertyValue<int>(propertyName, value);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002FC3 File Offset: 0x000011C3
		[Obsolete("Use GetPropertyValue<V> instead")]
		protected bool GetPropertyBoolValue(string propertyName)
		{
			return this.GetPropertyValue<bool>(propertyName, false);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002FCD File Offset: 0x000011CD
		[Obsolete("Use SetPropertyValue<V> instead")]
		protected void SetPropertyBoolValue(string propertyName, bool value)
		{
			this.SetPropertyValue<bool>(propertyName, value);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002FD7 File Offset: 0x000011D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeProfileBindings()
		{
			return false;
		}

		// Token: 0x04000013 RID: 19
		internal static string[] ForceSerializationProps = new string[]
		{
			"ClientStateFieldID"
		};

		// Token: 0x04000014 RID: 20
		private static string[] noSerializeProps = new string[]
		{
			"TargetControlID",
			"ProfileBindings"
		};

		// Token: 0x04000015 RID: 21
		private Dictionary<string, Control> findControlHelperCache = new Dictionary<string, Control>();

		// Token: 0x04000016 RID: 22
		private bool isDisposed;

		// Token: 0x04000017 RID: 23
		private ProfilePropertyBindingCollection profileBindings;
	}
}
