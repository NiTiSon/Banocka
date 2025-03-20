using BankAccountNS;
using NUnit.Framework;

namespace Bank;

public class Tests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void Debit_WithValidAmount_UpdatesBalance()
	{
		// Arrange
		double beginningBalance = 11.99;
		double debitAmount = 4.55;
		double expected = 7.44;
		BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

		// Act
		account.Debit(debitAmount);

		// Assert
		double actual = account.Balance;
		Assert.That(actual, Is.EqualTo(expected).Within(0.001), "Account not debited correctly");
	}
	
	[Test]
	public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
	{
		// Arrange
		double beginningBalance = 11.99;
		double debitAmount = 20.0;
		BankAccount account = new("Mr. Bryan Walton", beginningBalance);

		// Act
		try
		{
			account.Debit(debitAmount);
		}
		catch (ArgumentOutOfRangeException e)
		{
			// Assert
			StringAssert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, e.Message);
			return;
		}

		Assert.Fail("The expected exception was not thrown.");
	}

	[Test]
	public void AccountInitialize_WithValidNameAndBalance()
	{
		// Arrange
		string accountName = "Anton"; double balance = 4.55;
		BankAccount account = new(accountName, balance);

		// Assert
		Assert.That(balance, Is.EqualTo(account.Balance).Within(0.001), "Account name not initialized correctly");
		Assert.That(account.CustomerName, Is.EqualTo(accountName), "Account balance not initialized correctly");
	}

	[Test]
	public void Debit_WithInvalidAmount_ShouldThrowArgumentOutOfRange()
	{
		double beginningBalance = 11.99;
		BankAccount account = new("Mr. Anton Walton", beginningBalance);

		try
		{
			account.Debit(-0);
		}
		catch (ArgumentOutOfRangeException e)
		{
			StringAssert.Contains(BankAccount.DebitAmountLessThanZeroMessage, e.Message);
		}
		
		try
		{
			account.Debit(-20);
		}
		catch (ArgumentOutOfRangeException e)
		{
			StringAssert.Contains(BankAccount.DebitAmountLessThanZeroMessage, e.Message);
		}
		
		try
		{
			account.Debit(0);
		}
		catch (ArgumentOutOfRangeException e)
		{
			StringAssert.Contains(BankAccount.DebitAmountLessThanZeroMessage, e.Message);
		}
	}
	
	[Test]
	public void Credit_WithValidAmount_UpdatesBalance()
	{
		double beginningBalance = 11.99;
		double creditAmount = 4.55;
		BankAccount account = new("Anton", beginningBalance);
		
		account.Credit(creditAmount);
		
		Assert.That(beginningBalance + creditAmount, Is.EqualTo(account.Balance).Within(0.001), "Account not creditable correctly");
	}
}