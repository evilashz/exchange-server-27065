using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class AADDirectoryObjectPresentationObject : ConfigurableObject
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00005504 File Offset: 0x00003704
		internal AADDirectoryObjectPresentationObject(DirectoryObject directoryObject) : base(new SimpleProviderPropertyBag())
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
			AADDirectoryObjectPresentationObject[] members;
			if (directoryObject.members == null)
			{
				members = null;
			}
			else
			{
				members = (from member in directoryObject.members
				select AADPresentationObjectFactory.Create(member)).ToArray<AADDirectoryObjectPresentationObject>();
			}
			this.Members = members;
			this.ObjectId = directoryObject.objectId;
			this.ObjectType = directoryObject.objectType;
			AADDirectoryObjectPresentationObject[] owners;
			if (directoryObject.owners == null)
			{
				owners = null;
			}
			else
			{
				owners = (from owner in directoryObject.owners
				select AADPresentationObjectFactory.Create(owner)).ToArray<AADDirectoryObjectPresentationObject>();
			}
			this.Owners = owners;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000055BB File Offset: 0x000037BB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<AADDirectoryObjectPresentationObjectSchema>();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000055C2 File Offset: 0x000037C2
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ObjectId);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000055CF File Offset: 0x000037CF
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000055E1 File Offset: 0x000037E1
		public AADDirectoryObjectPresentationObject[] Members
		{
			get
			{
				return (AADDirectoryObjectPresentationObject[])this[AADDirectoryObjectPresentationObjectSchema.Members];
			}
			set
			{
				this[AADDirectoryObjectPresentationObjectSchema.Members] = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000055EF File Offset: 0x000037EF
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005601 File Offset: 0x00003801
		public string ObjectId
		{
			get
			{
				return (string)this[AADDirectoryObjectPresentationObjectSchema.ObjectId];
			}
			set
			{
				this[AADDirectoryObjectPresentationObjectSchema.ObjectId] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000560F File Offset: 0x0000380F
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005621 File Offset: 0x00003821
		public string ObjectType
		{
			get
			{
				return (string)this[AADDirectoryObjectPresentationObjectSchema.ObjectType];
			}
			set
			{
				this[AADDirectoryObjectPresentationObjectSchema.ObjectType] = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000562F File Offset: 0x0000382F
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005641 File Offset: 0x00003841
		public AADDirectoryObjectPresentationObject[] Owners
		{
			get
			{
				return (AADDirectoryObjectPresentationObject[])this[AADDirectoryObjectPresentationObjectSchema.Owners];
			}
			set
			{
				this[AADDirectoryObjectPresentationObjectSchema.Owners] = value;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000564F File Offset: 0x0000384F
		public override string ToString()
		{
			return this.ObjectId;
		}
	}
}
