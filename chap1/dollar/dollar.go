///
// File:  dollar.go
// Author: ymiyamoto
//
// Created on Mon Jun  4 02:11:23 2018
//
package dollar

type Dollar struct {
	amount int
}

func (d *Dollar) times(multiplier int) {
	d.amount *= multiplier
}
