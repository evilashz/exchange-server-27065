using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006DC RID: 1756
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SharepointObject : IReadOnlyPropertyBag
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x001293A8 File Offset: 0x001275A8
		internal SharepointObject(SharepointSiteId listId, SharepointSession session, XmlNode dataNode, Schema schema)
		{
			if (listId == null)
			{
				throw new ArgumentNullException();
			}
			if (session == null)
			{
				throw new ArgumentNullException();
			}
			if (dataNode == null)
			{
				throw new ArgumentNullException();
			}
			this.SharepointId = listId;
			this.Session = session;
			this.DataNode = dataNode.Clone();
			this.Schema = schema;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x001293F8 File Offset: 0x001275F8
		public static SharepointObject Read(SharepointSession session, ObjectId id)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			SharepointSiteId sharepointSiteId = id as SharepointSiteId;
			if (sharepointSiteId == null)
			{
				throw new ArgumentException("id");
			}
			if (sharepointSiteId is SharepointDocumentLibraryItemId)
			{
				return SharepointDocumentLibraryItem.Read(session, sharepointSiteId);
			}
			if (sharepointSiteId is SharepointListId)
			{
				return SharepointList.Read(session, sharepointSiteId);
			}
			throw new ObjectNotFoundException(sharepointSiteId, Strings.ExObjectNotFound(sharepointSiteId.ToString()));
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060045E1 RID: 17889 RVA: 0x00129467 File Offset: 0x00127667
		public DocumentLibraryObjectId Id
		{
			get
			{
				return this.SharepointId;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060045E2 RID: 17890
		public abstract SharepointItemType ItemType { get; }

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060045E3 RID: 17891
		public abstract string Title { get; }

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x0012946F File Offset: 0x0012766F
		public virtual Uri Uri
		{
			get
			{
				return this.SharepointId.SiteUri;
			}
		}

		// Token: 0x1700144B RID: 5195
		public object this[PropertyDefinition propDef]
		{
			get
			{
				object obj = this.TryGetProperty(propDef);
				if (obj is PropertyError)
				{
					throw PropertyErrorException.GetExceptionFromError((PropertyError)obj);
				}
				return obj;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x001294B0 File Offset: 0x001276B0
		object[] IReadOnlyPropertyBag.GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			if (propertyDefinitions == null)
			{
				return Array<object>.Empty;
			}
			object[] array = new object[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition propDef in propertyDefinitions)
			{
				array[num++] = this.TryGetProperty(propDef);
			}
			return array;
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x00129518 File Offset: 0x00127718
		protected GType GetValueOrDefault<GType>(PropertyDefinition propDef)
		{
			object obj = this.TryGetProperty(propDef);
			DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propDef as DocumentLibraryPropertyDefinition;
			if (obj is PropertyError)
			{
				obj = documentLibraryPropertyDefinition.DefaultValue;
			}
			return (GType)((object)obj);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0012954C File Offset: 0x0012774C
		public virtual object TryGetProperty(PropertyDefinition propDef)
		{
			DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propDef as DocumentLibraryPropertyDefinition;
			if (documentLibraryPropertyDefinition != null && documentLibraryPropertyDefinition.PropertyId == DocumentLibraryPropertyId.Id)
			{
				return this.Id;
			}
			return SharepointHelpers.GetValuesFromCAMLView(this.Schema, this.DataNode, this.CultureInfo, new PropertyDefinition[]
			{
				propDef
			})[0];
		}

		// Token: 0x04002646 RID: 9798
		internal readonly SharepointSiteId SharepointId;

		// Token: 0x04002647 RID: 9799
		protected internal readonly SharepointSession Session;

		// Token: 0x04002648 RID: 9800
		protected internal readonly XmlNode DataNode;

		// Token: 0x04002649 RID: 9801
		protected internal readonly Schema Schema;

		// Token: 0x0400264A RID: 9802
		protected internal CultureInfo CultureInfo;
	}
}
