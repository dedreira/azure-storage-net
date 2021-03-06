﻿//-----------------------------------------------------------------------
// <copyright file="FileProperties.cs" company="Microsoft">
//    Copyright 2013 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Azure.Storage.File
{
    using Microsoft.Azure.Storage.Core.Util;
    using Microsoft.Azure.Storage.Shared.Protocol;
    using System;

    /// <summary>
    /// Represents the system properties for a file.
    /// </summary>
    public sealed class FileProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileProperties"/> class.
        /// </summary>
        public FileProperties()
        {
            this.Length = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileProperties"/> class based on an existing instance.
        /// </summary>
        /// <param name="other">The set of file properties to clone.</param>
        public FileProperties(FileProperties other)
        {
            CommonUtility.AssertNotNull("other", other);

            this.ContentType = other.ContentType;
            this.ContentDisposition = other.ContentDisposition;
            this.ContentEncoding = other.ContentEncoding;
            this.ContentLanguage = other.ContentLanguage;
            this.CacheControl = other.CacheControl;
            this.ContentChecksum.MD5 = other.ContentChecksum.MD5;
            this.ContentChecksum.CRC64 = other.ContentChecksum.CRC64;
            this.Length = other.Length;
            this.ETag = other.ETag;
            this.LastModified = other.LastModified;
            this.IsServerEncrypted = other.IsServerEncrypted;
            this.filePermissionKey = other.filePermissionKey;
            this.ntfsAttributes = other.ntfsAttributes;
            this.creationTime = other.creationTime;
            this.lastWriteTime = other.lastWriteTime;
            this.filePermissionKeyToSet = other.filePermissionKeyToSet;
            this.ntfsAttributesToSet = other.ntfsAttributesToSet;
            this.creationTimeToSet = other.creationTimeToSet;
            this.lastWriteTimeToSet = other.lastWriteTimeToSet;
        }

        /// <summary>
        /// Gets or sets the cache-control value stored for the file.
        /// </summary>
        /// <value>The file's cache-control value.</value>
        public string CacheControl { get; set; }

        /// <summary>
        /// Gets or sets the content-disposition value stored for the file.
        /// </summary>
        /// <value>The file's content-disposition value.</value>
        /// <remarks>
        /// If this property has not been set for the file, it returns null.
        /// </remarks>
        public string ContentDisposition { get; set; }

        /// <summary>
        /// Gets or sets the content-encoding value stored for the file.
        /// </summary>
        /// <value>The file's content-encoding value.</value>
        /// <remarks>
        /// If this property has not been set for the file, it returns <c>null</c>.
        /// </remarks>
        public string ContentEncoding { get; set; }

        /// <summary>
        /// Gets or sets the content-language value stored for the file.
        /// </summary>
        /// <value>The file's content-language value.</value>
        /// <remarks>
        /// If this property has not been set for the file, it returns <c>null</c>.
        /// </remarks>
        public string ContentLanguage { get; set; }

        /// <summary>
        /// Gets the size of the file, in bytes.
        /// </summary>
        /// <value>The file's size in bytes.</value>
        public long Length { get; internal set; }

        /// <summary>
        /// Gets or sets the content-MD5 value stored for the file.
        /// </summary>
        /// <value>The file's content-MD5 hash.</value>
        public string ContentMD5
        {
            get => this.ContentChecksum.MD5;
            set => this.ContentChecksum.MD5 = value;
        }

        /// <summary>
        /// Gets or sets the content checksum value stored for the file.
        /// </summary>
        /// <value>A Checksum instance containing the blob's content checksum values.</value>
        internal Checksum ContentChecksum { get; set; } = new Checksum(md5: default(string), crc64: default(string));

        /// <summary>
        /// Gets or sets the content-type value stored for the file.
        /// </summary>
        /// <value>The file's content-type value.</value>
        /// <remarks>
        /// If this property has not been set for the file, it returns <c>null</c>.
        /// </remarks>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets the file's ETag value.
        /// </summary>
        /// <value>The file's ETag value.</value>
        public string ETag { get; internal set; }

        /// <summary>
        /// Gets the the last-modified time for the file, expressed as a UTC value.
        /// </summary>
        /// <value>The file's last-modified time, in UTC format.</value>
        public DateTimeOffset? LastModified { get; internal set; }

        /// <summary>
        /// Gets the file's server-side encryption state.
        /// </summary>
        /// <value>A bool representing the file's server-side encryption state.</value>
        public bool IsServerEncrypted { get; internal set; }

        /// <summary>
        /// The file's File Permission Key.
        /// </summary>
        internal string filePermissionKey;

        /// <summary>
        /// The file permission key to set on the next File Create or Set Properties call.
        /// </summary>
        internal string filePermissionKeyToSet;

        /// <summary>
        /// Gets or sets the file's File Permission Key.
        /// </summary>
        public string FilePermissionKey
        {
            get
            {
                return filePermissionKeyToSet ?? filePermissionKey;
            }
            set
            {
                filePermissionKeyToSet = value;
            }
        }

        /// <summary>
        /// Get or sets the file system attributes for files and directories.
        /// If not set, indicates preservation of existing values.
        /// </summary>
        internal CloudFileNtfsAttributes? ntfsAttributes;

        /// <summary>
        /// The NTFS file attributes to set on the new File Create or Set Properties call.
        /// </summary>
        internal CloudFileNtfsAttributes? ntfsAttributesToSet;

        /// <summary>
        /// Get or sets the file system attributes for files and directories.
        /// </summary>
        public CloudFileNtfsAttributes? NtfsAttributes
        {
            get
            {
                return ntfsAttributesToSet ?? ntfsAttributes;
            }
            set
            {
                ntfsAttributesToSet = value;
            }
        }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when the File was created.
        /// </summary>
        internal DateTimeOffset? creationTime;

        /// <summary>
        /// The file creation time to set on the next Create File or Set Properties call.
        /// </summary>
        internal DateTimeOffset? creationTimeToSet;

        /// <summary>
        /// Gets or sets the <see cref="DateTimeOffset"/> when the File was created.
        /// </summary>
        public DateTimeOffset? CreationTime
        {
            get
            {
                return creationTimeToSet ?? creationTime;
            }
            set
            {
                creationTimeToSet = value;
            }
        }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when the File was last written to.
        /// </summary>
        internal DateTimeOffset? lastWriteTime;

        /// <summary>
        /// The last write time to set on the next Create or Update Properties call.
        /// </summary>
        internal DateTimeOffset? lastWriteTimeToSet;

        /// <summary>
        /// Gets or sets the <see cref="DateTimeOffset"/> when the File was last written to.
        /// </summary>
        public DateTimeOffset? LastWriteTime
        {
            get
            {
                return lastWriteTimeToSet ?? lastWriteTime;
            }
            set
            {
                lastWriteTimeToSet = value;
            }
        }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when the File was last changed.  Read only.
        /// </summary>
        public DateTimeOffset? ChangeTime { get; internal set; }

        /// <summary>
        /// The File Id.  Read only.
        /// </summary>
        public string FileId { get; internal set; }

        /// <summary>
        /// The Id of this file's parent.  Ready only.
        /// </summary>
        public string ParentId { get; internal set; }
    }
}
