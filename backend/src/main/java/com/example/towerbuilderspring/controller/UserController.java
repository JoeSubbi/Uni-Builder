package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import com.example.towerbuilderspring.service.BuildingRequestValid;
import org.aspectj.asm.IModelFilter;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.*;

@RestController
@RequestMapping("/api")
public class UserController {

    @Autowired
    UserRepository userRepository;

    @GetMapping("/Users/")
    public ResponseEntity<List<Users>> getAllUsers() {
        try {
            List<Users> users = new ArrayList<Users>();
            userRepository.findAll().forEach(users::add);

            if (!users.isEmpty()) {
                return new ResponseEntity<>(users, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/Users/{id}")
    public ResponseEntity<Users> getUser(@PathVariable UUID id) {
        try {
            Users user = userRepository.findById(id).get();
            return new ResponseEntity<>(user, HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }


    @PostMapping("/Users/")
    public ResponseEntity<Users> createUser(@RequestBody Users user) {
        try {
            Users newUser = new Users(UUID.randomUUID(), user.getUserName(), user.getEmail(), user.getPassword(), user.getTotalExp(), user.getScore());
            userRepository.save(newUser);
            return new ResponseEntity<>(newUser, HttpStatus.CREATED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PutMapping("/Users/{id}")
    public ResponseEntity<Users> updateUser(@PathVariable("id") UUID id, @RequestBody Users user) {
        Optional<Users> userData = userRepository.findById(id);

        if (userData.isPresent()) {
            Users user_to_update = userData.get();
            user_to_update.setEmail(user.getEmail());
            user_to_update.setTotalExp(user.getTotalExp());
            user_to_update.setScore(user.getScore());

            return new ResponseEntity<>(userRepository.save(user_to_update), HttpStatus.OK);
        }
        else {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
    }

    @DeleteMapping("/Users/{id}")
    public ResponseEntity<Users> deleteUser(@PathVariable("id") UUID id) {
        try {
            userRepository.deleteById(id);
            return new ResponseEntity<>(HttpStatus.ACCEPTED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/Users/{id}/Buildings")
    public ResponseEntity<Set<BuildingModels>> getUserBuildings(@PathVariable("id") UUID id) {
        try {
            Optional<Users> fetched_user = userRepository.findById(id);
            if (fetched_user.isPresent()) {
                Users user = fetched_user.get();
                return new ResponseEntity<>(user.getUserBuildings(), HttpStatus.ACCEPTED);
            }
            else {
                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/Users/{id}/Buildings")
    public ResponseEntity<List<Object>> addUserBuilding(@PathVariable("id") UUID id,
                                                          @RequestBody BuildingModels building) {
        try {
            Optional<Users> fetched_user = userRepository.findById(id);
            if (fetched_user.isPresent()) {
                Users user = fetched_user.get();
                // TODO Create new validator service here.
                user.addUserBuilding(building);
                List<Object> userAndBuildingAdded = Arrays.asList(user, building);
                return new ResponseEntity<>(userAndBuildingAdded, HttpStatus.OK);

            } else {
                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

//    @PostMapping("/User/{userId}/Buildings/{buildingId}")
//    public ResponseEntity<List<Object>> changeUserBuilding(@PathVariable("userId") UUID userId,
//                                                           @PathVariable("buildingID") long buildingId,
//                                                           @RequestBody BuildingModels building) {
//        try {
//            BuildingRequestValid validator = new BuildingRequestValid();
//            List<Object> result = validator.validate(userId, buildingId);
//            if (result != null) {
//                Users user = (Users) result.get(0);
//                BuildingModels previousBuilding = (BuildingModels) result.get(1);
//
//                // Find the current user model of the same group and replace it.
//
//
//            } else {
//                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
//            }
//        } catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }

}
