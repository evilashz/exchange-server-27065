using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D3 RID: 1747
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UncDocumentLibrary : IDocumentLibrary, IReadOnlyPropertyBag
	{
		// Token: 0x060045B5 RID: 17845 RVA: 0x00128620 File Offset: 0x00126820
		private UncDocumentLibrary(UncSession session, UncObjectId id, string description)
		{
			if (id.Path.Segments.Length != 2)
			{
				throw new ArgumentException("id");
			}
			this.shareName = Uri.UnescapeDataString(id.Path.Segments[1]);
			this.description = description;
			this.id = id;
			this.session = session;
			this.directoryInfo = new DirectoryInfo(this.Uri.LocalPath);
			this.directoryInfo.Refresh();
			FileAttributes attributes = this.directoryInfo.Attributes;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0012881C File Offset: 0x00126A1C
		public static UncDocumentLibrary Read(UncSession session, ObjectId objectId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			UncObjectId uncObjectId = objectId as UncObjectId;
			if (uncObjectId == null || uncObjectId.UriFlags != UriFlags.UncDocumentLibrary)
			{
				throw new ArgumentException("objectId");
			}
			if (!session.Uri.IsBaseOf(uncObjectId.Path))
			{
				throw new ArgumentException("objectId");
			}
			if (uncObjectId.Path.Segments.Length != 2)
			{
				throw new ArgumentException("uncId");
			}
			return Utils.DoUncTask<UncDocumentLibrary>(session.Identity, uncObjectId, false, Utils.MethodType.Read, delegate
			{
				string netName = Uri.UnescapeDataString(uncObjectId.Path.Segments[1].TrimEnd(new char[]
				{
					'\\',
					'/'
				}));
				IntPtr zero = IntPtr.Zero;
				int num = UncSession.NetShareGetInfo(uncObjectId.Path.Host, netName, 1, out zero);
				try
				{
					if (num == 0)
					{
						UncSession.SHARE_INFO_1 share_INFO_ = (UncSession.SHARE_INFO_1)Marshal.PtrToStructure(zero, typeof(UncSession.SHARE_INFO_1));
						try
						{
							return new UncDocumentLibrary(session, uncObjectId, share_INFO_.Remark);
						}
						catch (IOException innerException)
						{
							throw new AccessDeniedException(uncObjectId, Strings.ExAccessDenied(uncObjectId.Path.LocalPath), innerException);
						}
					}
					if (num == 5 || num == 2311)
					{
						throw new AccessDeniedException(uncObjectId, Strings.ExAccessDenied(uncObjectId.Path.LocalPath));
					}
					if (num == 53 || num == 2250)
					{
						throw new ConnectionException(uncObjectId, Strings.ExCannotConnectToMachine(uncObjectId.Path.Host));
					}
					throw new ObjectNotFoundException(uncObjectId, Strings.ExObjectNotFound(uncObjectId.Path.LocalPath));
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						UncSession.NetApiBufferFree(zero);
					}
				}
				UncDocumentLibrary result;
				return result;
			});
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x001288F0 File Offset: 0x00126AF0
		public ObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x001288F8 File Offset: 0x00126AF8
		public string Title
		{
			get
			{
				return this.shareName;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x00128900 File Offset: 0x00126B00
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x00128908 File Offset: 0x00126B08
		public Uri Uri
		{
			get
			{
				return this.id.Path;
			}
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00128918 File Offset: 0x00126B18
		public List<KeyValuePair<string, Uri>> GetHierarchy()
		{
			List<KeyValuePair<string, Uri>> list = new List<KeyValuePair<string, Uri>>(this.id.Path.Segments.Length - 1);
			string uriString = "\\\\" + this.id.Path.Host;
			list.Add(new KeyValuePair<string, Uri>(this.id.Path.Host, new Uri(uriString)));
			return list;
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x0012897C File Offset: 0x00126B7C
		IDocumentLibraryItem IDocumentLibrary.Read(ObjectId objectId, params PropertyDefinition[] propsToReturn)
		{
			return this.Read(objectId, propsToReturn);
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00128988 File Offset: 0x00126B88
		public UncDocumentLibraryItem Read(ObjectId objectId, params PropertyDefinition[] propsToReturn)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			UncObjectId uncObjectId = objectId as UncObjectId;
			if (uncObjectId == null)
			{
				throw new ArgumentException("objectId");
			}
			if (uncObjectId.Path.Segments.Length <= 2)
			{
				throw new ArgumentException("objectId");
			}
			if (!this.id.Path.IsBaseOf(uncObjectId.Path))
			{
				throw new ArgumentException("objectId");
			}
			return UncDocumentLibraryItem.Read(this.session, uncObjectId);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x00128A02 File Offset: 0x00126C02
		public ITableView GetView(QueryFilter query, SortBy[] sortBy, DocumentLibraryQueryOptions queryOptions, params PropertyDefinition[] propsToReturn)
		{
			return UncDocumentLibraryFolder.InternalGetView(query, sortBy, queryOptions, propsToReturn, this.session, this.directoryInfo, this.id);
		}

		// Token: 0x1700143F RID: 5183
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				object obj = this.TryGetProperty(propertyDefinition);
				if (obj is PropertyError)
				{
					throw PropertyErrorException.GetExceptionFromError((PropertyError)obj);
				}
				return obj;
			}
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x00128A4C File Offset: 0x00126C4C
		object[] IReadOnlyPropertyBag.GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			object[] array = new object[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				array[num++] = this.TryGetProperty(propertyDefinition);
			}
			return array;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x00128AAC File Offset: 0x00126CAC
		private object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			DocumentLibraryPropertyId documentLibraryPropertyId = DocumentLibraryPropertyId.None;
			DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propertyDefinition as DocumentLibraryPropertyDefinition;
			if (documentLibraryPropertyDefinition != null)
			{
				documentLibraryPropertyId = documentLibraryPropertyDefinition.PropertyId;
			}
			DocumentLibraryPropertyId documentLibraryPropertyId2 = documentLibraryPropertyId;
			switch (documentLibraryPropertyId2)
			{
			case DocumentLibraryPropertyId.None:
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			case DocumentLibraryPropertyId.Uri:
				return this.id.Path;
			default:
				switch (documentLibraryPropertyId2)
				{
				case DocumentLibraryPropertyId.Id:
					return this.id;
				case DocumentLibraryPropertyId.Title:
					return this.shareName;
				default:
					if (documentLibraryPropertyId2 != DocumentLibraryPropertyId.Description)
					{
						return UncDocumentLibraryItem.GetValueFromFileSystemInfo(documentLibraryPropertyDefinition, this.directoryInfo);
					}
					return this.description;
				}
				break;
			}
		}

		// Token: 0x04002621 RID: 9761
		private readonly UncObjectId id;

		// Token: 0x04002622 RID: 9762
		private readonly string shareName;

		// Token: 0x04002623 RID: 9763
		private readonly string description;

		// Token: 0x04002624 RID: 9764
		private readonly UncSession session;

		// Token: 0x04002625 RID: 9765
		private readonly DirectoryInfo directoryInfo;
	}
}
