using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	internal class AirSyncDataObject : PropertyBase, IPropertyContainer, IProperty, IDataObjectGeneratorContainer
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00059A90 File Offset: 0x00057C90
		public AirSyncDataObject(IList<IProperty> propertyFromSchemaLinkId, IAirSyncMissingPropertyStrategy missingPropStrategy, IAirSyncDataObjectGenerator schemaState)
		{
			if (propertyFromSchemaLinkId == null)
			{
				throw new ArgumentNullException("propertyFromSchemaLinkId");
			}
			if (missingPropStrategy == null)
			{
				throw new ArgumentNullException("missingPropStrategy");
			}
			this.missingPropStrategy = missingPropStrategy;
			this.propertyFromSchemaLinkId = propertyFromSchemaLinkId;
			this.schemaState = schemaState;
			this.propertyFromAirSyncTag = new Dictionary<string, AirSyncProperty>(propertyFromSchemaLinkId.Count);
			for (int i = 0; i < propertyFromSchemaLinkId.Count; i++)
			{
				AirSyncProperty airSyncProperty = (AirSyncProperty)propertyFromSchemaLinkId[i];
				if (airSyncProperty == null)
				{
					throw new ArgumentNullException("airSyncProperty");
				}
				if (airSyncProperty.AirSyncTagNames == null)
				{
					throw new ArgumentNullException("airSyncProperty.AirSyncTagNames");
				}
				for (int j = 0; j < airSyncProperty.AirSyncTagNames.Length; j++)
				{
					if (airSyncProperty.AirSyncTagNames[j] != null)
					{
						this.propertyFromAirSyncTag[airSyncProperty.AirSyncTagNames[j]] = airSyncProperty;
					}
				}
			}
			this.missingPropStrategy.Validate(this);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00059B61 File Offset: 0x00057D61
		private AirSyncDataObject()
		{
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00059B69 File Offset: 0x00057D69
		public IList<IProperty> Children
		{
			get
			{
				return this.propertyFromSchemaLinkId;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00059B71 File Offset: 0x00057D71
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00059B79 File Offset: 0x00057D79
		public IDataObjectGenerator SchemaState
		{
			get
			{
				return this.schemaState;
			}
			set
			{
				this.schemaState = (value as IAirSyncDataObjectGenerator);
			}
		}

		// Token: 0x17000602 RID: 1538
		public AirSyncProperty this[string airSyncTagName]
		{
			get
			{
				return this.propertyFromAirSyncTag[airSyncTagName];
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00059B98 File Offset: 0x00057D98
		public void Bind(XmlNode xmlItemRoot)
		{
			if (xmlItemRoot == null)
			{
				throw new ArgumentNullException("xmlItemRoot");
			}
			foreach (object obj in xmlItemRoot.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					throw new ConversionException("Unexpected node type, should have been caught in a higher validation layer");
				}
				AirSyncProperty airSyncProperty = null;
				if (!this.propertyFromAirSyncTag.TryGetValue(xmlNode.Name, out airSyncProperty))
				{
					throw new ConversionException("Cannot bind property to XML node " + xmlNode.Name + ", property does not exist");
				}
				airSyncProperty.Bind(xmlNode);
			}
			foreach (AirSyncProperty airSyncProperty2 in this.propertyFromAirSyncTag.Values)
			{
				if (airSyncProperty2.State == PropertyState.Uninitialized)
				{
					airSyncProperty2.BindToParent(xmlItemRoot);
				}
			}
			this.missingPropStrategy.PostProcessPropertyBag(this);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00059CAC File Offset: 0x00057EAC
		public override void CopyFrom(IProperty srcRootProperty)
		{
			IPropertyContainer propertyContainer = srcRootProperty as IPropertyContainer;
			if (propertyContainer == null)
			{
				throw new UnexpectedTypeException("IPropertyContainer", srcRootProperty);
			}
			propertyContainer.SetCopyDestination(this);
			foreach (IProperty property in propertyContainer.Children)
			{
				if (property.State != PropertyState.NotSupported)
				{
					AirSyncProperty propBySchemaLinkId = this.GetPropBySchemaLinkId(property.SchemaLinkId);
					if (propBySchemaLinkId is IDataObjectGeneratorContainer)
					{
						((IDataObjectGeneratorContainer)propBySchemaLinkId).SchemaState = this.schemaState;
						((IDataObjectGeneratorContainer)property).SchemaState = ((IDataObjectGeneratorContainer)srcRootProperty).SchemaState;
					}
					this.missingPropStrategy.ExecuteCopyProperty(property, propBySchemaLinkId);
				}
			}
			foreach (IProperty property2 in propertyContainer.Children)
			{
				if (property2.State != PropertyState.NotSupported)
				{
					AirSyncProperty propBySchemaLinkId2 = this.GetPropBySchemaLinkId(property2.SchemaLinkId);
					if (propBySchemaLinkId2 != null && propBySchemaLinkId2.PostProcessingDelegate != null)
					{
						propBySchemaLinkId2.PostProcessingDelegate(property2, this.Children);
						propBySchemaLinkId2.PostProcessingDelegate = null;
					}
				}
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00059DE0 File Offset: 0x00057FE0
		public void SetCopyDestination(IPropertyContainer dstPropertyContainer)
		{
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00059DE4 File Offset: 0x00057FE4
		public AirSyncProperty TryGetAirSyncPropertyByName(string propertyName)
		{
			AirSyncProperty result = null;
			if (this.propertyFromAirSyncTag.TryGetValue(propertyName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00059E08 File Offset: 0x00058008
		public override void Unbind()
		{
			base.Unbind();
			foreach (AirSyncProperty airSyncProperty in this.propertyFromAirSyncTag.Values)
			{
				airSyncProperty.Unbind();
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00059E68 File Offset: 0x00058068
		private AirSyncProperty GetPropBySchemaLinkId(int schemaLinkId)
		{
			return (AirSyncProperty)this.propertyFromSchemaLinkId[schemaLinkId];
		}

		// Token: 0x04000A70 RID: 2672
		private IAirSyncMissingPropertyStrategy missingPropStrategy;

		// Token: 0x04000A71 RID: 2673
		private Dictionary<string, AirSyncProperty> propertyFromAirSyncTag;

		// Token: 0x04000A72 RID: 2674
		private IList<IProperty> propertyFromSchemaLinkId;

		// Token: 0x04000A73 RID: 2675
		private IAirSyncDataObjectGenerator schemaState;
	}
}
