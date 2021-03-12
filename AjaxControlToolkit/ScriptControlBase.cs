using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200001C RID: 28
	public class ScriptControlBase : ScriptControl, INamingContainer, IControlResolver, IPostBackDataHandler, ICallbackEventHandler, IClientStateManager
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000355D File Offset: 0x0000175D
		public ScriptControlBase(HtmlTextWriterTag tag) : this(false, tag)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003567 File Offset: 0x00001767
		protected ScriptControlBase() : this(false)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003570 File Offset: 0x00001770
		protected ScriptControlBase(string tag) : this(false, tag)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000357A File Offset: 0x0000177A
		protected ScriptControlBase(bool enableClientState)
		{
			this.enableClientState = enableClientState;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003589 File Offset: 0x00001789
		protected ScriptControlBase(bool enableClientState, HtmlTextWriterTag tag)
		{
			this.tagKey = tag;
			this.enableClientState = enableClientState;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000359F File Offset: 0x0000179F
		protected ScriptControlBase(bool enableClientState, string tag)
		{
			this.tagKey = HtmlTextWriterTag.Unknown;
			this.tagName = tag;
			this.enableClientState = enableClientState;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000035BC File Offset: 0x000017BC
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000035DC File Offset: 0x000017DC
		[DefaultValue("")]
		public virtual string ScriptPath
		{
			get
			{
				return (string)(this.ViewState["ScriptPath"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ScriptPath"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000035F0 File Offset: 0x000017F0
		protected virtual string ClientControlType
		{
			get
			{
				ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
				return clientScriptResourceAttribute.ComponentType;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000361E File Offset: 0x0000181E
		protected virtual bool SupportsClientState
		{
			get
			{
				return this.enableClientState;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003626 File Offset: 0x00001826
		protected ScriptManager ScriptManager
		{
			get
			{
				this.EnsureScriptManager();
				return this.scriptManager;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003634 File Offset: 0x00001834
		protected string ClientStateFieldID
		{
			get
			{
				if (this.cachedClientStateFieldID == null)
				{
					this.cachedClientStateFieldID = this.ClientID + "_ClientState";
				}
				return this.cachedClientStateFieldID;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000365A File Offset: 0x0000185A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.tagKey;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003664 File Offset: 0x00001864
		protected override string TagName
		{
			get
			{
				if (this.tagName == null && this.TagKey != HtmlTextWriterTag.Unknown)
				{
					this.tagName = Enum.Format(typeof(HtmlTextWriterTag), this.TagKey, "G").ToLower(CultureInfo.InvariantCulture);
				}
				return this.tagName;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000036B6 File Offset: 0x000018B6
		private void EnsureScriptManager()
		{
			if (this.scriptManager == null)
			{
				this.scriptManager = ScriptManager.GetCurrent(this.Page);
				if (this.scriptManager == null)
				{
					throw new HttpException(Resources.E_NoScriptManager);
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000036E4 File Offset: 0x000018E4
		public override Control FindControl(string id)
		{
			Control control = base.FindControl(id);
			if (control != null)
			{
				return control;
			}
			for (Control namingContainer = this.NamingContainer; namingContainer != null; namingContainer = namingContainer.NamingContainer)
			{
				control = namingContainer.FindControl(id);
				if (control != null)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003720 File Offset: 0x00001920
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.EnsureID();
			this.EnsureScriptManager();
			if (this.SupportsClientState)
			{
				this.Page.ClientScript.RegisterHiddenField(this.ClientStateFieldID, this.SaveClientState());
				this.Page.RegisterRequiresPostBack(this);
			}
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003776 File Offset: 0x00001976
		protected virtual void LoadClientState(string clientState)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003778 File Offset: 0x00001978
		protected virtual string SaveClientState()
		{
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000377C File Offset: 0x0000197C
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (this.SupportsClientState)
			{
				string text = postCollection[this.ClientStateFieldID];
				if (!string.IsNullOrEmpty(text))
				{
					this.LoadClientState(text);
				}
			}
			return false;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000037AE File Offset: 0x000019AE
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000037B0 File Offset: 0x000019B0
		protected sealed override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (!this.Visible)
			{
				return null;
			}
			base.EnsureID();
			List<ScriptDescriptor> list = this.CreateScriptDescriptors();
			this.BuildScriptDescriptor((ScriptComponentDescriptor)list[0]);
			return list;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000037E8 File Offset: 0x000019E8
		protected virtual List<ScriptDescriptor> CreateScriptDescriptors()
		{
			List<ScriptDescriptor> list = new List<ScriptDescriptor>(1);
			ScriptControlDescriptor item = new ScriptControlDescriptor(this.ClientControlType, this.ClientID);
			list.Add(item);
			return list;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003816 File Offset: 0x00001A16
		protected virtual void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003818 File Offset: 0x00001A18
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.Visible)
			{
				return null;
			}
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(ScriptObjectBuilder.GetScriptReferences(base.GetType()));
			if (this.ScriptPath.Length > 0)
			{
				list.Add(new ScriptReference(this.ScriptPath));
			}
			return list;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003868 File Offset: 0x00001A68
		protected virtual string GetCallbackResult()
		{
			string text = this.callbackArgument;
			this.callbackArgument = null;
			return ScriptObjectBuilder.ExecuteCallbackMethod(this, text);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000388A File Offset: 0x00001A8A
		protected virtual void RaiseCallbackEvent(string eventArgument)
		{
			this.callbackArgument = eventArgument;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003893 File Offset: 0x00001A93
		public Control ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000389C File Offset: 0x00001A9C
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000038A6 File Offset: 0x00001AA6
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000038AE File Offset: 0x00001AAE
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.GetCallbackResult();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000038B6 File Offset: 0x00001AB6
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.RaiseCallbackEvent(eventArgument);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000038BF File Offset: 0x00001ABF
		bool IClientStateManager.SupportsClientState
		{
			get
			{
				return this.SupportsClientState;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000038C7 File Offset: 0x00001AC7
		void IClientStateManager.LoadClientState(string clientState)
		{
			this.LoadClientState(clientState);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000038D0 File Offset: 0x00001AD0
		string IClientStateManager.SaveClientState()
		{
			return this.SaveClientState();
		}

		// Token: 0x0400002D RID: 45
		private ScriptManager scriptManager;

		// Token: 0x0400002E RID: 46
		private bool enableClientState;

		// Token: 0x0400002F RID: 47
		private string cachedClientStateFieldID;

		// Token: 0x04000030 RID: 48
		private string callbackArgument;

		// Token: 0x04000031 RID: 49
		private string tagName;

		// Token: 0x04000032 RID: 50
		private HtmlTextWriterTag tagKey;
	}
}
