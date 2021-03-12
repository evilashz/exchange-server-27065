using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200004C RID: 76
	internal class Resource : IResource
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00007F90 File Offset: 0x00006190
		public Resource(string selfUri)
		{
			if (!string.IsNullOrEmpty(selfUri))
			{
				this.links.Add(new Link("self", selfUri, "self"));
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00007FE1 File Offset: 0x000061E1
		public ICollection<Link> Links
		{
			get
			{
				return new LinksCollection(this.links);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008000 File Offset: 0x00006200
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00008058 File Offset: 0x00006258
		public string SelfUri
		{
			get
			{
				Link link2 = (from link in this.links
				where link.Relationship == "self"
				select link).FirstOrDefault<Link>();
				if (link2 == null)
				{
					return null;
				}
				return link2.Href;
			}
			set
			{
				this.links.RemoveAll((Link link) => link.Token == "self");
				this.links.Add(new Link("ignore-this-token", value, "self"));
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000080A9 File Offset: 0x000062A9
		// (set) Token: 0x06000260 RID: 608 RVA: 0x000080B1 File Offset: 0x000062B1
		public Dictionary<string, object> MultipartAttachments
		{
			get
			{
				return this.multipartAttachments;
			}
			set
			{
				this.multipartAttachments = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000080BA File Offset: 0x000062BA
		public virtual bool CanBeEmbedded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000080BD File Offset: 0x000062BD
		protected ICollection<string> Keys
		{
			get
			{
				return this.properties.Keys;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000080CC File Offset: 0x000062CC
		public static Resource FromDictionary(Type resourceType, Dictionary<string, object> dictionary)
		{
			dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
			Resource resource = Activator.CreateInstance(resourceType, new object[]
			{
				string.Empty
			}) as Resource;
			if (resource != null)
			{
				resource.properties = dictionary;
				if (dictionary.ContainsKey("_links"))
				{
					IDictionary dictionary2 = dictionary["_links"] as IDictionary;
					if (dictionary2 != null)
					{
						foreach (object obj in dictionary2.Keys)
						{
							string text = (string)obj;
							IEnumerable enumerable = dictionary2[text] as IEnumerable;
							IDictionary dictionary3 = enumerable as IDictionary;
							if (dictionary3 != null)
							{
								enumerable = new IDictionary[]
								{
									dictionary3
								};
							}
							foreach (object obj2 in enumerable)
							{
								IDictionary dictionary4 = (IDictionary)obj2;
								string href = dictionary4["href"] as string;
								resource.links.Add(new Link(text, href, text));
							}
						}
					}
				}
				if (dictionary.ContainsKey("_embedded"))
				{
					dictionary["_embedded"] = Resource.ConvertDictionaryToCaseInsensitive(dictionary["_embedded"]);
				}
				resource.PopulateDataFromDictionary();
			}
			return resource;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008250 File Offset: 0x00006450
		public void AddLink(string relationship, string uri, object target = null)
		{
			this.links.Add(new Link(relationship, uri, relationship)
			{
				Target = target
			});
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000082D4 File Offset: 0x000064D4
		public virtual object ToDictionary(List<EmbeddedPart> mimeParts)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			foreach (IGrouping<string, Link> grouping in from link in this.links
			group link by link.Token)
			{
				if (grouping.All((Link link) => link.CanBeEmbedded))
				{
					object value = Resource.UnwrapIfOneItem(grouping.Select((Link link) => ((Resource)link.Target).ToDictionary(mimeParts)).ToArray<object>());
					dictionary3.Add(grouping.Key, value);
				}
				else
				{
					foreach (Link link2 in grouping)
					{
						if (link2.Target is ExternalResource && mimeParts != null)
						{
							ExternalResource externalResource = (ExternalResource)link2.Target;
							string text = externalResource.ContentId ?? Guid.NewGuid().ToString();
							mimeParts.Add(new EmbeddedPart
							{
								Content = externalResource.Value,
								ContentId = text
							});
							link2.Href = string.Format("{0}{1}", "cid:", text);
						}
					}
					object value2 = Resource.UnwrapIfOneItem(grouping.Select((Link link) => new Dictionary<string, string>
					{
						{
							"href",
							link.Href
						}
					}).ToArray<Dictionary<string, string>>());
					dictionary2.Add(grouping.Key, value2);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.properties)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			if (dictionary2 != null)
			{
				dictionary["_links"] = dictionary2;
			}
			if (dictionary3.Count > 0)
			{
				dictionary["_embedded"] = dictionary3;
			}
			else
			{
				dictionary.Remove("_embedded");
			}
			return dictionary;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008578 File Offset: 0x00006778
		public virtual void PopulateDataFromDictionary()
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000857C File Offset: 0x0000677C
		protected T GetValue<T>(string name)
		{
			object value = this.GetValue(typeof(T), name);
			if (value == null)
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000085B0 File Offset: 0x000067B0
		protected void SetValue<T>(string name, object value)
		{
			Type typeFromHandle = typeof(T);
			this.SetValue(name, value, typeFromHandle);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000085D4 File Offset: 0x000067D4
		private static object ConvertDictionaryToCaseInsensitive(object dict)
		{
			IDictionary dictionary = dict as IDictionary;
			if (dictionary != null)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
				foreach (object obj in dictionary.Keys)
				{
					string key = (string)obj;
					dictionary2[key] = dictionary[key];
				}
				return dictionary2;
			}
			return dict;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008650 File Offset: 0x00006850
		private static object ConvertTo(Type type, object value)
		{
			object result = null;
			if (value == null)
			{
				return null;
			}
			if (type.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
			{
				return value;
			}
			string text = value.ToString();
			if (type == typeof(DateTime))
			{
				if (text != null)
				{
					result = DateTime.Parse(text);
				}
			}
			else if (type == typeof(TimeSpan))
			{
				if (text != null)
				{
					result = TimeSpan.Parse(text);
				}
			}
			else if (type.GetTypeInfo().IsEnum)
			{
				if (text != null)
				{
					result = Enum.Parse(type, text);
				}
			}
			else if (type == typeof(bool))
			{
				if (text != null)
				{
					result = bool.Parse(text);
				}
			}
			else if (type == typeof(int))
			{
				if (text != null)
				{
					result = int.Parse(text);
				}
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000874C File Offset: 0x0000694C
		private static Resource ResourceCollectionFromDictionary(Type collectionType, IEnumerable collectionElements)
		{
			Type type = collectionType;
			while (type != null && type.GetTypeInfo().BaseType != typeof(Resource))
			{
				type = type.GetTypeInfo().BaseType;
			}
			Type typeArg = type.GetGenericArguments().First<Type>();
			IResourceCollection resourceCollection = (IResourceCollection)Activator.CreateInstance(collectionType);
			Resource[] array = (from dict in collectionElements.OfType<Dictionary<string, object>>()
			select Resource.FromDictionary(typeArg, dict)).ToArray<Resource>();
			foreach (Resource target in array)
			{
				resourceCollection.AddItem(target);
			}
			return resourceCollection as Resource;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000087FC File Offset: 0x000069FC
		private static object UnwrapIfOneItem(object value)
		{
			if (value is IEnumerable && !(value is string))
			{
				IEnumerable<object> source = ((IEnumerable)value).Cast<object>();
				if (source.Any<object>() && !source.Skip(1).Any<object>())
				{
					return source.First<object>();
				}
			}
			return value;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000885C File Offset: 0x00006A5C
		private object GetValue(Type t, string name)
		{
			object obj2 = null;
			if (t == typeof(string))
			{
				return this.GetRawValue<string>(name);
			}
			if (t == typeof(int))
			{
				obj2 = this.GetRawValue<int>(name);
			}
			else if (t == typeof(bool))
			{
				obj2 = this.GetRawValue<bool>(name);
			}
			else if (t == typeof(DateTime))
			{
				obj2 = this.GetRawValue<DateTime>(name);
			}
			else if (t == typeof(TimeSpan))
			{
				string rawValue = this.GetRawValue<string>(name);
				if (rawValue != null)
				{
					obj2 = TimeSpan.Parse(rawValue);
				}
			}
			else if (t.GetTypeInfo().IsEnum)
			{
				string rawValue2 = this.GetRawValue<string>(name);
				if (rawValue2 != null)
				{
					obj2 = Enum.Parse(t, this.GetRawValue<string>(name), true);
				}
			}
			else if (t.GetTypeInfo().IsGenericType && t.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if (!this.IsValueSet(name))
				{
					obj2 = null;
				}
				else
				{
					Type t2 = t.GetGenericArguments().First<Type>();
					obj2 = this.GetValue(t2, name);
				}
			}
			else if (typeof(IResource).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
			{
				obj2 = this.GetResource(name, t);
			}
			else if (t.IsArray)
			{
				IEnumerable rawValue3 = this.GetRawValue<IEnumerable>(name);
				if (rawValue3 != null)
				{
					Type elementType = t.GetElementType();
					object[] array = (from obj in rawValue3.OfType<object>()
					select Resource.ConvertTo(elementType, obj)).ToArray<object>();
					Array array2 = Array.CreateInstance(elementType, array.Length);
					array.CopyTo(array2, 0);
					obj2 = array2;
				}
			}
			if (obj2 != null)
			{
				return obj2;
			}
			return null;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008A44 File Offset: 0x00006C44
		private void SetValue(string name, object value, Type t)
		{
			if (value == null)
			{
				this.SetRawValue(name, null);
				return;
			}
			if (t == typeof(string) || t.GetTypeInfo().IsPrimitive || t == typeof(DateTime) || t == typeof(bool))
			{
				this.SetRawValue(name, value);
				return;
			}
			if (typeof(IResource).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
			{
				this.SetResource(name, value);
				return;
			}
			if (t.IsArray)
			{
				Type elementType = t.GetElementType();
				if (elementType.GetTypeInfo().IsEnum)
				{
					string[] value2 = (from e in ((Array)value).OfType<Enum>()
					select e.ToString()).ToArray<string>();
					this.SetRawValue(name, value2);
					return;
				}
				IEnumerable enumerable = value as IEnumerable;
				List<string> list = new List<string>();
				foreach (object obj in enumerable)
				{
					list.Add(obj.ToString());
				}
				this.SetRawValue(name, list.ToArray());
				return;
			}
			else
			{
				if (t.GetTypeInfo().IsGenericType && t.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					Type t2 = t.GetGenericArguments().First<Type>();
					this.SetValue(name, value, t2);
					return;
				}
				this.SetRawValue(name, value.ToString());
				return;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008BE4 File Offset: 0x00006DE4
		private bool IsValueSet(string name)
		{
			return this.properties.ContainsKey(name) && this.properties[name] != null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008C08 File Offset: 0x00006E08
		private T GetRawValue<T>(string name)
		{
			object obj = null;
			if (!this.properties.TryGetValue(name, out obj))
			{
				return default(T);
			}
			if (obj != null && !(obj is T))
			{
				if (typeof(T) == typeof(int) && obj is string)
				{
					obj = int.Parse((string)obj);
				}
				else if (typeof(T) == typeof(bool) && obj is string)
				{
					obj = bool.Parse((string)obj);
				}
				else
				{
					if (!(typeof(T) == typeof(DateTime)) || !(obj is string))
					{
						throw new ArgumentException("The property value is not valid for the specified type");
					}
					obj = DateTime.Parse((string)obj);
				}
			}
			return (T)((object)obj);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008CF5 File Offset: 0x00006EF5
		private void SetRawValue(string name, object value)
		{
			if (value != null)
			{
				this.properties[name] = value;
				return;
			}
			this.properties.Remove(name);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008D30 File Offset: 0x00006F30
		private void SetResource(string name, object value)
		{
			this.links.RemoveAll((Link link) => link.Relationship == name);
			if (value != null)
			{
				this.AddLink(name, ((IResource)value).SelfUri, value);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008D98 File Offset: 0x00006F98
		private object GetResource(string name, Type resourceType)
		{
			Link link4 = (from link in this.Links
			where link.Relationship == name
			select link).FirstOrDefault<Link>();
			if (link4 == null && this.properties.ContainsKey("_embedded"))
			{
				IDictionary dictionary = this.properties["_embedded"] as IDictionary;
				if (dictionary != null && dictionary.Contains(name))
				{
					Resource resource;
					if (typeof(IResourceCollection).GetTypeInfo().IsAssignableFrom(resourceType.GetTypeInfo()))
					{
						resource = Resource.ResourceCollectionFromDictionary(resourceType, dictionary[name] as IEnumerable);
					}
					else
					{
						resource = Resource.FromDictionary(resourceType, dictionary[name] as Dictionary<string, object>);
					}
					Link link2 = new Link(name, resource.SelfUri, name)
					{
						Target = resource
					};
					this.Links.Add(link2);
					link4 = link2;
				}
			}
			if (link4 == null && typeof(ExternalResource).GetTypeInfo().IsAssignableFrom(resourceType.GetTypeInfo()))
			{
				string rawValue = this.GetRawValue<string>(name);
				if (rawValue != null && rawValue.StartsWith("cid:", StringComparison.OrdinalIgnoreCase))
				{
					string key = rawValue.Substring(4);
					if (this.multipartAttachments != null && this.multipartAttachments.ContainsKey(key))
					{
						object value = this.multipartAttachments[key];
						Link link3 = new Link(name, rawValue, name)
						{
							Target = new ExternalResource
							{
								Value = value
							}
						};
						this.Links.Add(link3);
						link4 = link3;
					}
				}
			}
			if (link4 == null)
			{
				return null;
			}
			IResource resource2 = link4.Target as IResource;
			if (resource2 == null)
			{
				return null;
			}
			return resource2;
		}

		// Token: 0x04000185 RID: 389
		private readonly List<Link> links = new List<Link>();

		// Token: 0x04000186 RID: 390
		private Dictionary<string, object> properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000187 RID: 391
		private Dictionary<string, object> multipartAttachments;

		// Token: 0x0200004D RID: 77
		public static class StringConstants
		{
			// Token: 0x0400018E RID: 398
			public const string Self = "self";

			// Token: 0x0400018F RID: 399
			public const string Links = "_links";

			// Token: 0x04000190 RID: 400
			public const string Embedded = "_embedded";

			// Token: 0x04000191 RID: 401
			public const string CidPrefix = "cid:";

			// Token: 0x04000192 RID: 402
			public const string Href = "href";

			// Token: 0x04000193 RID: 403
			public const string Rel = "rel";

			// Token: 0x04000194 RID: 404
			public const string Title = "title";

			// Token: 0x04000195 RID: 405
			public const string OperationId = "operationId";
		}
	}
}
