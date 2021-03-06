﻿using System;
using Novell.Directory.Ldap.NETStandard.FunctionalTests.Helpers;
using Xunit;

namespace Novell.Directory.Ldap.NETStandard.FunctionalTests
{
    public class DeleteEntryTests
    {
        [Fact]
        public void Delete_OfExistingEntry_ShouldWork()
        {
            var existingEntry = LdapOps.AddEntry();

            TestHelper.WithAuthenticatedLdapConnection(ldapConnection => { ldapConnection.Delete(existingEntry.DN); });

            var retrivedEntry = LdapOps.GetEntry(existingEntry.DN);
            Assert.Null(retrivedEntry);
        }

        [Fact]
        public void Delete_OfNotExistingEntry_ShouldThrownNoSuchObject()
        {
            var ldapException = Assert.Throws<LdapException>(
                () => TestHelper.WithAuthenticatedLdapConnection(ldapConnection => { ldapConnection.Delete(TestHelper.BuildDn(Guid.NewGuid().ToString())); })
            );
            Assert.Equal(LdapException.NO_SUCH_OBJECT, ldapException.ResultCode);          
        }
    }
}