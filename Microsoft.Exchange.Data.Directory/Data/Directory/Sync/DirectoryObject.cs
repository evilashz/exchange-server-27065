using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200083D RID: 2109
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlInclude(typeof(PublicFolder))]
	[XmlInclude(typeof(Device))]
	[XmlInclude(typeof(KeyGroup))]
	[XmlInclude(typeof(ServicePrincipal))]
	[XmlInclude(typeof(User))]
	[XmlInclude(typeof(Subscription))]
	[XmlInclude(typeof(SubscribedPlan))]
	[XmlInclude(typeof(Group))]
	[XmlInclude(typeof(ForeignPrincipal))]
	[XmlInclude(typeof(Contact))]
	[XmlInclude(typeof(Company))]
	[XmlInclude(typeof(Account))]
	[Serializable]
	public abstract class DirectoryObject
	{
		// Token: 0x060068B5 RID: 26805 RVA: 0x0017120C File Offset: 0x0016F40C
		internal static DirectoryObjectClass GetObjectClass(DirectoryObjectClassAddressList restriction)
		{
			switch (restriction)
			{
			case DirectoryObjectClassAddressList.Contact:
				return DirectoryObjectClass.Contact;
			case DirectoryObjectClassAddressList.Group:
				return DirectoryObjectClass.Group;
			case DirectoryObjectClassAddressList.User:
				return DirectoryObjectClass.User;
			default:
			{
				string message = string.Format(CultureInfo.CurrentCulture, "The value '{0}' is invalid.", new object[]
				{
					restriction.ToString()
				});
				throw new ArgumentException(message, "restriction");
			}
			}
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x00171268 File Offset: 0x0016F468
		internal static DirectoryObjectClass GetObjectClass(DirectoryObjectClassPerson restriction)
		{
			switch (restriction)
			{
			case DirectoryObjectClassPerson.Contact:
				return DirectoryObjectClass.Contact;
			case DirectoryObjectClassPerson.User:
				return DirectoryObjectClass.User;
			default:
			{
				string message = string.Format(CultureInfo.CurrentCulture, "The value '{0}' is invalid.", new object[]
				{
					restriction.ToString()
				});
				throw new ArgumentException(message, "restriction");
			}
			}
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x001712C0 File Offset: 0x0016F4C0
		internal static DirectoryProperty GetDirectoryProperty(object nonDirectoryProperty)
		{
			if (nonDirectoryProperty == null)
			{
				return null;
			}
			if (nonDirectoryProperty is AttributeSet[])
			{
				DirectoryPropertyAttributeSet directoryPropertyAttributeSet = new DirectoryPropertyAttributeSet();
				directoryPropertyAttributeSet.SetValues((AttributeSet[])nonDirectoryProperty);
				return directoryPropertyAttributeSet;
			}
			string message = string.Format(CultureInfo.CurrentCulture, "The value '{0}' is invalid.", new object[]
			{
				nonDirectoryProperty.GetType()
			});
			throw new ArgumentException(message, "nonDirectoryProperty");
		}

		// Token: 0x060068B8 RID: 26808
		internal abstract void ForEachProperty(IPropertyProcessor processor);

		// Token: 0x060068B9 RID: 26809 RVA: 0x0017131A File Offset: 0x0016F51A
		public DirectoryObject()
		{
			this.allField = false;
			this.deletedField = false;
		}

		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x060068BA RID: 26810 RVA: 0x00171330 File Offset: 0x0016F530
		// (set) Token: 0x060068BB RID: 26811 RVA: 0x00171338 File Offset: 0x0016F538
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

		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x060068BC RID: 26812 RVA: 0x00171341 File Offset: 0x0016F541
		// (set) Token: 0x060068BD RID: 26813 RVA: 0x00171349 File Offset: 0x0016F549
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

		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x060068BE RID: 26814 RVA: 0x00171352 File Offset: 0x0016F552
		// (set) Token: 0x060068BF RID: 26815 RVA: 0x0017135A File Offset: 0x0016F55A
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

		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x060068C0 RID: 26816 RVA: 0x00171363 File Offset: 0x0016F563
		// (set) Token: 0x060068C1 RID: 26817 RVA: 0x0017136B File Offset: 0x0016F56B
		[XmlAttribute]
		[DefaultValue(false)]
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

		// Token: 0x040044D8 RID: 17624
		private bool allField;

		// Token: 0x040044D9 RID: 17625
		private string contextIdField;

		// Token: 0x040044DA RID: 17626
		private string objectIdField;

		// Token: 0x040044DB RID: 17627
		private bool deletedField;
	}
}
