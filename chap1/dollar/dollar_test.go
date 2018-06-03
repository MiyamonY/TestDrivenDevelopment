///
// File:  chap1.go
// Author: ymiyamoto
//
// Created on Mon Jun  4 00:37:06 2018
//
package dollar

import "testing"

const msg string = "Error:expcted %d, actual %d"

func TestMultiplication(t *testing.T) {
	five := &Dollar{5}

	five.times(2)
	if five.amount != 10 {
		t.Errorf(msg, 10, five.amount)
	}
}
