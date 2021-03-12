using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200000D RID: 13
	internal abstract class ActivityManagerConfig : ActivityConfig
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004C86 File Offset: 0x00002E86
		internal ActivityManagerConfig(ActivityManagerConfig manager) : base(manager)
		{
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004C8F File Offset: 0x00002E8F
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004C97 File Offset: 0x00002E97
		internal ActivityConfig InitialActivity
		{
			get
			{
				return this.firstActivityConfig;
			}
			set
			{
				this.firstActivityConfig = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004CA0 File Offset: 0x00002EA0
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004CA8 File Offset: 0x00002EA8
		internal Type FsmProxyType
		{
			get
			{
				return this.fsmProxyType;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004CB0 File Offset: 0x00002EB0
		internal static string BuildScopedConfigMapId(ActivityManagerConfig config, string id)
		{
			return config.UniqueId + "::" + id;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004CC4 File Offset: 0x00002EC4
		internal ActivityConfig GetScopedConfig(string id)
		{
			ActivityConfig activityConfig = (ActivityConfig)ActivityManagerConfig.scopedConfigMap[ActivityManagerConfig.BuildScopedConfigMapId(this, id)];
			if (activityConfig == null)
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Request for unknown scoped activityid: {0}.", new object[]
				{
					id
				});
				throw new FsmConfigurationException(Strings.UnknownTransitionId(id, base.ActivityId));
			}
			return activityConfig;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004D1F File Offset: 0x00002F1F
		internal bool TryGetScopedConfig(string id, out ActivityConfig config)
		{
			config = (ActivityConfig)ActivityManagerConfig.scopedConfigMap[ActivityManagerConfig.BuildScopedConfigMapId(this, id)];
			return config != null;
		}

		// Token: 0x060000E7 RID: 231
		internal abstract ActivityManager CreateActivityManager(ActivityManager manager);

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D41 File Offset: 0x00002F41
		internal override ActivityBase CreateActivity(ActivityManager manager)
		{
			return this.CreateActivityManager(manager);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004D4C File Offset: 0x00002F4C
		internal override void Load(XmlNode rootNode)
		{
			this.className = rootNode.LocalName;
			Type type = GlobalActivityManager.ConfigClass.CoreAssembly.GetType("Microsoft.Exchange.UM.UMCore." + this.className);
			this.fsmProxyType = GlobalActivityManager.ConfigClass.CoreAssembly.GetType("Microsoft.Exchange.UM.Fsm." + this.className);
			if (null == type || null == this.fsmProxyType)
			{
				throw new FsmConfigurationException(Strings.InvalidActivityManager(rootNode.Name));
			}
			if (this is GlobalActivityManager.ConfigClass)
			{
				ActivityManagerConfig.scopedConfigMap.Clear();
			}
			base.Load(rootNode);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004DE8 File Offset: 0x00002FE8
		protected override void LoadChildren(XmlNode rootNode)
		{
			foreach (object obj in rootNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string text = string.Intern(xmlNode.Name);
				string text2 = (xmlNode.Attributes != null && xmlNode.Attributes["id"] != null) ? string.Intern(xmlNode.Attributes["id"].Value) : string.Empty;
				string key;
				switch (key = text)
				{
				case "Menu":
					this.AddScopedConfig(new MenuConfig(this), text2);
					continue;
				case "SpeechMenu":
					this.AddScopedConfig(new SpeechMenuConfig(this), text2);
					continue;
				case "Record":
					this.AddScopedConfig(new RecordConfig(this), text2);
					continue;
				case "CallTransfer":
					this.AddScopedConfig(new CallTransferConfig(this), text2);
					continue;
				case "PlayBackMenu":
					this.AddScopedConfig(new MenuConfig(this), text2);
					continue;
				case "FaxRequest":
					this.AddScopedConfig(new FaxRequestConfig(this), text2);
					continue;
				}
				if (text.EndsWith("Manager"))
				{
					string text3 = typeof(ActivityManagerConfig).Namespace + "." + text + "+ConfigClass";
					Type type = Type.GetType(text3, true);
					ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(ActivityManagerConfig)
					}, null);
					if (constructor == null)
					{
						throw new FsmConfigurationException(Strings.UnKnownManager(text3));
					}
					this.AddScopedConfig((ActivityManagerConfig)constructor.Invoke(new object[]
					{
						this
					}), text2);
				}
			}
			foreach (object obj2 in rootNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				string text = string.Intern(xmlNode2.Name);
				if (string.Equals(text, "Transition", StringComparison.OrdinalIgnoreCase))
				{
					base.ParseTransitionNode(xmlNode2, this);
				}
				else
				{
					XmlAttribute xmlAttribute = (xmlNode2.Attributes == null) ? null : xmlNode2.Attributes["id"];
					string text2 = (xmlAttribute == null) ? null : string.Intern(xmlAttribute.Value);
					if (!string.IsNullOrEmpty(text2) && text2 != "0")
					{
						this.GetScopedConfig(text2).Load(xmlNode2);
					}
				}
			}
			try
			{
				string text2 = string.Intern(rootNode.Attributes["firstActivityId"].Value);
				this.firstActivityConfig = this.GetScopedConfig(text2);
			}
			catch (FsmConfigurationException innerException)
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Unknown first activity in activity manager {0}.", new object[]
				{
					this
				});
				throw new FsmConfigurationException(Strings.UnknownFirstActivityId(this.ToString()), innerException);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005198 File Offset: 0x00003398
		private void AddScopedConfig(ActivityConfig config, string id)
		{
			string text = ActivityManagerConfig.BuildScopedConfigMapId(this, id);
			if (ActivityManagerConfig.scopedConfigMap.ContainsKey(text))
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Duplicate scoped config id {0} found in xml configuration file.", new object[]
				{
					text
				});
				throw new FsmConfigurationException(Strings.DuplicateScopedId(text));
			}
			ActivityManagerConfig.scopedConfigMap[text] = config;
		}

		// Token: 0x04000045 RID: 69
		private static Hashtable scopedConfigMap = new Hashtable();

		// Token: 0x04000046 RID: 70
		private ActivityConfig firstActivityConfig;

		// Token: 0x04000047 RID: 71
		private string className;

		// Token: 0x04000048 RID: 72
		private Type fsmProxyType;
	}
}
