using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D1 RID: 1745
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UncDocumentLibraryItem : IDocumentLibraryItem, IReadOnlyPropertyBag
	{
		// Token: 0x0600459A RID: 17818 RVA: 0x00127D90 File Offset: 0x00125F90
		internal UncDocumentLibraryItem(UncSession session, UncObjectId uncObjectId, FileSystemInfo fileSystemInfo, Schema schema)
		{
			this.session = session;
			this.uncId = uncObjectId;
			this.fileSystemInfo = fileSystemInfo;
			this.schema = schema;
			try
			{
				this.fileSystemInfo.Refresh();
				if (this.fileSystemInfo.Attributes == (FileAttributes)(-1))
				{
					throw new ObjectNotFoundException(uncObjectId, Strings.ExObjectNotFound(uncObjectId.Path.LocalPath));
				}
			}
			catch (ArgumentException innerException)
			{
				throw new ObjectNotFoundException(uncObjectId, Strings.ExObjectNotFound(uncObjectId.Path.LocalPath), innerException);
			}
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x00127ECC File Offset: 0x001260CC
		public static UncDocumentLibraryItem Read(UncSession session, ObjectId id)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			UncObjectId uncObjectId = id as UncObjectId;
			if (uncObjectId == null)
			{
				throw new ArgumentException("id");
			}
			if (uncObjectId.UriFlags != UriFlags.UncDocument && uncObjectId.UriFlags != UriFlags.UncFolder)
			{
				throw new ArgumentException("id");
			}
			if (!session.Uri.IsBaseOf(uncObjectId.Path))
			{
				throw new ArgumentException("objectId");
			}
			return Utils.DoUncTask<UncDocumentLibraryItem>(session.Identity, uncObjectId, false, Utils.MethodType.Read, delegate
			{
				FileSystemInfo fileSystemInfo = new FileInfo(uncObjectId.Path.LocalPath);
				if (fileSystemInfo.Attributes == (FileAttributes)(-1))
				{
					throw new ObjectNotFoundException(uncObjectId, Strings.ExObjectNotFound(uncObjectId.Path.LocalPath));
				}
				if (fileSystemInfo.Exists)
				{
					return new UncDocument(session, uncObjectId);
				}
				if ((fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
				{
					return new UncDocumentLibraryFolder(session, uncObjectId);
				}
				throw new ObjectNotFoundException(uncObjectId, Strings.ExObjectNotFound(uncObjectId.Path.LocalPath));
			});
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x00127F9B File Offset: 0x0012619B
		public virtual string DisplayName
		{
			get
			{
				return this.fileSystemInfo.Name;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x00127FA8 File Offset: 0x001261A8
		public Uri Uri
		{
			get
			{
				return this.uncId.Path;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x00127FB5 File Offset: 0x001261B5
		public ObjectId Id
		{
			get
			{
				return this.uncId;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x00127FBD File Offset: 0x001261BD
		public bool IsFolder
		{
			get
			{
				return (this.fileSystemInfo.Attributes & FileAttributes.Directory) != (FileAttributes)0;
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x00127FD3 File Offset: 0x001261D3
		IDocumentLibrary IDocumentLibraryItem.Library
		{
			get
			{
				return this.Library;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x00127FDB File Offset: 0x001261DB
		IDocumentLibraryFolder IDocumentLibraryItem.Parent
		{
			get
			{
				return this.Parent;
			}
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x00127FE4 File Offset: 0x001261E4
		public List<KeyValuePair<string, Uri>> GetHierarchy()
		{
			List<KeyValuePair<string, Uri>> list = new List<KeyValuePair<string, Uri>>(this.uncId.Path.Segments.Length - 1);
			string text = "\\\\" + this.uncId.Path.Host;
			list.Add(new KeyValuePair<string, Uri>(this.uncId.Path.Host, new Uri(text)));
			for (int i = 1; i < this.uncId.Path.Segments.Length - 1; i++)
			{
				string text2 = this.uncId.Path.Segments[i].TrimEnd(new char[]
				{
					'/',
					'\\'
				});
				text2 = Uri.UnescapeDataString(text2);
				text = Path.Combine(text, text2);
				list.Add(new KeyValuePair<string, Uri>(text2, new Uri(text)));
			}
			return list;
		}

		// Token: 0x17001435 RID: 5173
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
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x001280EC File Offset: 0x001262EC
		public object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propertyDefinition as DocumentLibraryPropertyDefinition;
			if (documentLibraryPropertyDefinition == null)
			{
				throw new ArgumentException("propertyDefinition");
			}
			return this.TryGetProperty(documentLibraryPropertyDefinition);
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00128124 File Offset: 0x00126324
		object[] IReadOnlyPropertyBag.GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			if (propertyDefinitions == null)
			{
				return Array<object>.Empty;
			}
			object[] array = new object[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				array[num++] = this.TryGetProperty(propertyDefinition);
			}
			return array;
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x0012818C File Offset: 0x0012638C
		public UncDocumentLibrary Library
		{
			get
			{
				if (this.documentLibrary == null)
				{
					string text = Path.Combine("\\\\" + this.uncId.Path.Host, this.uncId.Path.Segments[1].TrimEnd(new char[]
					{
						'\\',
						'/'
					}));
					text = Uri.UnescapeDataString(text);
					UncObjectId objectId = new UncObjectId(new Uri(text), UriFlags.UncDocumentLibrary);
					this.documentLibrary = UncDocumentLibrary.Read(this.session, objectId);
				}
				return this.documentLibrary;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x0012825C File Offset: 0x0012645C
		public UncDocumentLibraryFolder Parent
		{
			get
			{
				if (!this.parentInitialized)
				{
					this.parent = Utils.DoUncTask<UncDocumentLibraryFolder>(this.session.Identity, this.UncId, false, Utils.MethodType.Read, delegate
					{
						string parentDirectoryNameInternal = this.GetParentDirectoryNameInternal();
						if (parentDirectoryNameInternal != null)
						{
							Uri uri = new Uri(parentDirectoryNameInternal);
							if (uri.Segments.Length > 2)
							{
								return new UncDocumentLibraryFolder(this.session, new UncObjectId(new Uri(parentDirectoryNameInternal), UriFlags.UncFolder));
							}
						}
						return null;
					});
					this.parentInitialized = true;
				}
				return this.parent;
			}
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x001282B0 File Offset: 0x001264B0
		internal static object GetValueFromFileSystemInfo(DocumentLibraryPropertyDefinition propertyDefinition, FileSystemInfo fileSystemInfo)
		{
			FileInfo fileInfo = fileSystemInfo as FileInfo;
			switch (propertyDefinition.PropertyId)
			{
			case DocumentLibraryPropertyId.Uri:
				return new Uri(fileSystemInfo.FullName);
			case DocumentLibraryPropertyId.CreationTime:
				return fileSystemInfo.CreationTimeUtc;
			case DocumentLibraryPropertyId.LastModifiedTime:
				return fileSystemInfo.LastWriteTimeUtc;
			case DocumentLibraryPropertyId.IsFolder:
				return (fileSystemInfo.Attributes & FileAttributes.Directory) != (FileAttributes)0;
			case DocumentLibraryPropertyId.IsHidden:
				return (fileSystemInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
			case DocumentLibraryPropertyId.Id:
				return new UncObjectId(new Uri(fileSystemInfo.FullName), ((fileSystemInfo.Attributes & FileAttributes.Directory) != (FileAttributes)0) ? UriFlags.UncFolder : UriFlags.UncDocument);
			case DocumentLibraryPropertyId.Title:
				return fileSystemInfo.Name;
			case DocumentLibraryPropertyId.FileSize:
				if (fileInfo != null)
				{
					return fileInfo.Length;
				}
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			case DocumentLibraryPropertyId.FileType:
				if (fileInfo == null)
				{
					return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
				}
				if (!string.IsNullOrEmpty(fileInfo.Extension))
				{
					return ExtensionToContentTypeMapper.Instance.GetContentTypeByExtension(fileInfo.Extension.Substring(1));
				}
				return "application/octet-stream";
			case DocumentLibraryPropertyId.BaseName:
				return fileSystemInfo.Name;
			}
			throw PropertyErrorException.GetExceptionFromError(new PropertyError(propertyDefinition, PropertyErrorCode.NotSupported));
		}

		// Token: 0x060045AA RID: 17834
		protected abstract string GetParentDirectoryNameInternal();

		// Token: 0x060045AB RID: 17835 RVA: 0x001283F2 File Offset: 0x001265F2
		public virtual object TryGetProperty(DocumentLibraryPropertyDefinition propertyDefinition)
		{
			if (this.schema.AllProperties.ContainsKey(propertyDefinition))
			{
				return UncDocumentLibraryItem.GetValueFromFileSystemInfo(propertyDefinition, this.fileSystemInfo);
			}
			return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x0012841B File Offset: 0x0012661B
		internal UncObjectId UncId
		{
			get
			{
				return this.uncId;
			}
		}

		// Token: 0x04002619 RID: 9753
		private readonly Schema schema;

		// Token: 0x0400261A RID: 9754
		private readonly UncObjectId uncId;

		// Token: 0x0400261B RID: 9755
		protected UncSession session;

		// Token: 0x0400261C RID: 9756
		private UncDocumentLibraryFolder parent;

		// Token: 0x0400261D RID: 9757
		private bool parentInitialized;

		// Token: 0x0400261E RID: 9758
		protected FileSystemInfo fileSystemInfo;

		// Token: 0x0400261F RID: 9759
		private UncDocumentLibrary documentLibrary;
	}
}
