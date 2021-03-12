using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000183 RID: 387
	[XmlInclude(typeof(Company1))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(StockKeepingUnit))]
	[XmlInclude(typeof(ServicePlan))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(FeatureDescriptor))]
	[XmlInclude(typeof(KeyGroup))]
	[XmlInclude(typeof(SliceInstance))]
	[XmlInclude(typeof(ServicePrincipal))]
	[XmlInclude(typeof(Datacenter))]
	[XmlInclude(typeof(User1))]
	[XmlInclude(typeof(ThrottlePolicy))]
	[XmlInclude(typeof(TaskSet))]
	[XmlInclude(typeof(Task))]
	[XmlInclude(typeof(Subscription1))]
	[XmlInclude(typeof(SubscribedPlan))]
	[XmlInclude(typeof(ServiceInstance))]
	[XmlInclude(typeof(Service))]
	[XmlInclude(typeof(Scope))]
	[XmlInclude(typeof(RoleTemplate))]
	[XmlInclude(typeof(Role))]
	[XmlInclude(typeof(Region))]
	[XmlInclude(typeof(Group))]
	[XmlInclude(typeof(ForeignPrincipal))]
	[XmlInclude(typeof(Contract))]
	[XmlInclude(typeof(Contact))]
	[XmlInclude(typeof(Account1))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryObject
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0002089F File Offset: 0x0001EA9F
		public DirectoryObject()
		{
			this.allField = false;
			this.deletedField = false;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000208B5 File Offset: 0x0001EAB5
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x000208BD File Offset: 0x0001EABD
		[XmlAttribute]
		[DefaultValue(false)]
		public bool All
		{
			get
			{
				return this.allField;
			}
			set
			{
				this.allField = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000208C6 File Offset: 0x0001EAC6
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x000208CE File Offset: 0x0001EACE
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000208D7 File Offset: 0x0001EAD7
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x000208DF File Offset: 0x0001EADF
		[XmlAttribute]
		public string ObjectId
		{
			get
			{
				return this.objectIdField;
			}
			set
			{
				this.objectIdField = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000208E8 File Offset: 0x0001EAE8
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x000208F0 File Offset: 0x0001EAF0
		[DefaultValue(false)]
		[XmlAttribute]
		public bool Deleted
		{
			get
			{
				return this.deletedField;
			}
			set
			{
				this.deletedField = value;
			}
		}

		// Token: 0x0400046D RID: 1133
		private bool allField;

		// Token: 0x0400046E RID: 1134
		private string contextIdField;

		// Token: 0x0400046F RID: 1135
		private string objectIdField;

		// Token: 0x04000470 RID: 1136
		private bool deletedField;
	}
}
