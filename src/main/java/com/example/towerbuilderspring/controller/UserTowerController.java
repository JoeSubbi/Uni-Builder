package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.UserTowers;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserRepository;
import com.example.towerbuilderspring.repository.UserTowerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.*;

import java.util.ArrayList;
import java.util.List;


@RestController
@RequestMapping("/api")
public class UserTowerController {

    @Autowired
    UserTowerRepository userTowerRepository;


    // User Tower Table code
    @GetMapping("/UserTowers/")
    public ResponseEntity<List<UserTowers>> getAllUserTowers() {
        try {
            List<UserTowers> userTowers = new ArrayList<UserTowers>();
            userTowerRepository.findAll().forEach(userTowers::add);

            if (!userTowers.isEmpty()) {
                return new ResponseEntity<>(userTowers, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    // When placing data in the database need to check that each value belongs in the respective table.
    

//    @PostMapping("/UserTowers/")
//    public ResponseEntity<UserTowers> createUserTower(@RequestBody UserTowers tower) {
//        try {
//            UserTowers newTower = new UserTowers(tower.getTowerId(), tower.getName(), tower.getUser(), tower.getModels(), tower.getColours());
//            userTowerRepository.save(newTower);
//            return new ResponseEntity<>(newTower, HttpStatus.CREATED);
//        }
//        catch (Exception e) {
//            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }


    // This code must also incoperate some backend checks first before changing data.

//    @PutMapping("/UserTower/{id}")
//    public ResponseEntity<UserTowers> updateUserTower(@PathVariable("id") long id, @RequestBody UserTowers userTower) {
//
//        Optional<UserTowers> userTowerData = userTowerRepository.findById(id);
//
//        if (userTowerData.isPresent()) {
//            UserTowers tower_to_update = userTowerData.get();
//            tower_to_update.setTowerId(id);
//            tower_to_update.setName(userTower.getName());
//            tower_to_update.setUser(userTower.getUser());
//            tower_to_update.setModels(userTowerModels);
//        }
//    }
}
